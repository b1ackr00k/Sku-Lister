using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Sku_Lister
{
    public partial class Form1 : Form
    {
        
        //string list for logging
        List<Object> log = new List<Object>();

        //make an empty string for the filename which is accessible at the class level
        string fileName = "";
        bool taskIsRunning; 

        CancellationTokenSource cts = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = button1;
            throbber.Hide();
        }

        public void scrapBtn_Click(object sender, EventArgs e)
        {
            //hide the grid
            skuGrid.Hide();

            //show the throbber
            throbber.Show();

            //clear datagrid view
            skuGrid.Rows.Clear();
            skuGrid.Refresh();

            //only launch the scrape if a file has been added
            if (fileName != "")
            {
                Log("File name: " + fileName);
                cts = new CancellationTokenSource();

                CancellationToken token = cts.Token;

                taskIsRunning = true;

                Task t = Task.Factory.StartNew(
                () =>
                {
                    Scrape(token,fileName);
                },
                token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
                );
            }
            else
            {
                MessageBox.Show("You must first add a file to scrape");
            }
        }
        

        private void Scrape(CancellationToken token, string path)
        {
            PdfReader reader = new PdfReader(path);
            string sUri = "";

            try
            {
                // Pagination
                for (var i = 1; i <= reader.NumberOfPages; i++)
                {
                    //get current page
                    var pageDict = reader.GetPageN(i);

                    //get all annotations from current page
                    var annotArray = (PdfArray)PdfReader.GetPdfObject(pageDict.Get(PdfName.ANNOTS));

                    //ensure array isn't empty
                    if (annotArray == null) continue;
                    if (annotArray.Length <= 0) continue;

                    // check every annotation on the page
                    foreach (var annot in annotArray.ArrayList)
                    {
                        //convert the iTextSharp-specific object to a generic PDF object
                        var annotDict = (PdfDictionary)PdfReader.GetPdfObject(annot);

                        //ensure the object isnt empty
                        if (annotDict == null) continue;

                        //get the annotation subtype and ensure it is a link
                        var subtype = annotDict.Get(PdfName.SUBTYPE).ToString();
                        Log("Subtype: " + subtype);
                        if (subtype != "/Link") continue;


                        //get the annotations ACTION
                        var linkDict = (PdfDictionary)annotDict.GetDirectObject(PdfName.A);
                        if (linkDict == null) continue;

                        if (!linkDict.Get(PdfName.S).Equals(PdfName.GOTO))
                        {
                            //get the link from the annotation
                            sUri = linkDict.Get(PdfName.URI).ToString();
                            Log("URI: " + sUri);
                            if (String.IsNullOrEmpty(sUri)) continue; 
                        }
                        

                        //build the link address into a string
                        string linkTextBuilder;

                        //create a rectangle, define its paramteres, read the text under the rectangle (the anchor text for the link) and write it to the string
                        var LinkLocation = annotDict.GetAsArray(PdfName.RECT);
                        List<string> linestringlist = new List<string>();
                        iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(((PdfNumber)LinkLocation[0]).FloatValue, ((PdfNumber)LinkLocation[1]).FloatValue, ((PdfNumber)LinkLocation[2]).FloatValue, ((PdfNumber)LinkLocation[3]).FloatValue);
                        RenderFilter[] renderFilter = new RenderFilter[1];
                        renderFilter[0] = new RegionTextRenderFilter(rect);
                        ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);
                        linkTextBuilder = PdfTextExtractor.GetTextFromPage(reader, i, textExtractionStrategy).Trim();
                        Log(linkTextBuilder);

                        String sUriHTTPSPrefix = sUri.Substring(0, 5);
                        String sUriWWWPrefix = sUri.Substring(0, 3);

                        if (sUriHTTPSPrefix.Equals("https"))
                        {
                            Log("Prefix: " + sUriHTTPSPrefix);
                        }
                        else if (sUriWWWPrefix.Equals("www"))
                        {
                            Log("Prefix: " + sUriWWWPrefix);
                        }


                        if (sUri.Length.Equals(70) && sUriHTTPSPrefix.Equals("https"))
                        {
                            //instantiate the web request and response objects
                            WebRequest httpReq = WebRequest.Create(sUri);
                            WebResponse response = httpReq.GetResponse();

                            //check website response from request and save status to string
                            string webStatus = ((HttpWebResponse)response).StatusDescription.ToString();
                            Log("Webstatus: " + webStatus);

                            //check website response url from request and save url to string
                            string responseURL = ((HttpWebResponse)response).ResponseUri.ToString();
                            Log("Response URI: " + responseURL);

                            //split the response url string to just sku number after the "="
                            string webSite = responseURL.Split('=')[1];
                            Log("Response SKU: " + webSite);

                            //split the link harvested from the annotations in the pdf after the "="
                            string sku = sUri.Split('=')[1];
                            Log("PDF SKU: " + sku);

                            //truncate the split string to just the sku (removes any extra symbols, such as copywright, which were captured in the rectangle)
                            string finalSku = sku.Substring(0, 7);
                            Log("PDF SKU Final: " + finalSku);

                            //delete asteriks from sku
                            var deleteChars = new string[] { "*" };
                            foreach (var c in deleteChars)
                            {
                                linkTextBuilder = finalSku.Replace(c, string.Empty);
                            }

                            //truncate the split string to just the sku (removes any extra symbols, such as copywright, which were captured in the rectangle)
                            string linkText = linkTextBuilder.Substring(0, 7);
                            Log("Link SKU Final: " + linkText);
                            
                            //default status of string match is "NO MATCH"
                            string match = "\tNO MATCH";

                            //make a blank IPEndPoint
                            IPEndPoint remoteEP = null;

                            //create a new httpwebrequest
                            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sUri);

                            //bind IPEndPoint from the remote end point to a variable
                            req.ServicePoint.BindIPEndPointDelegate = delegate (ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
                            {
                                remoteEP = remoteEndPoint;
                                return null;
                            };

                            //get the response from the request
                            req.GetResponse();
                            Log("HTTP response: " + req.GetResponse());

                            //set remoteEndPoint to string for display
                            string hostIP = remoteEP.Address.ToString();
                            Log("Host IP: " + hostIP);

                            //stop the request to make way for a new request on next iteration
                            req.Abort();

                            //if sku from PDF link, website response link, and pdf anchor text all match then change match
                            if (linkText.Equals(sku) && webSite.Equals(sku))
                            {
                                match = "MATCH";
                            }

                            //add data to datagridview
                            this.skuGrid.Rows.Add(i, sku, linkTextBuilder, webSite, match, webStatus, hostIP);
                            //close http request and response
                            response.Close();

                            if (token.IsCancellationRequested)
                            {
                                // Clean up here, then...
                                token.ThrowIfCancellationRequested();
                                Log("Cancellation token: " + token);
                                MessageBox.Show("Scrape stopped");
                                throbber.Hide();
                                skuGrid.Show();
                            }
                        }
                        else if (!sUri.Length.Equals(70) && sUriWWWPrefix.Equals("www") ||
                            !sUri.Length.Equals(70) && sUriHTTPSPrefix.Equals("https"))
                        {
                            //instantiate the web request and response objects
                            WebRequest httpReq = WebRequest.Create(sUri);
                            WebResponse response = httpReq.GetResponse();

                            //check website response from request and save status to string
                            string webStatus = ((HttpWebResponse)response).StatusDescription.ToString();
                            Log("Web Status: " + webStatus);

                            //check website response url from request and save url to string
                            string responseURL = ((HttpWebResponse)response).ResponseUri.ToString();
                            Log("Response URI: " + responseURL);

                            //split the response url string to just sku number after the "="
                            string webSite = responseURL;
                            Log("Response SKU: " + webSite);

                            //default status of string match is "NO MATCH"
                            string match = "\tNO MATCH";
                            
                            //make a blank IPEndPoint
                            IPEndPoint remoteEP = null;
                            
                            //create a new httpwebrequest
                            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sUri);

                            //bind IPEndPoint from the remote end point to a variable
                            req.ServicePoint.BindIPEndPointDelegate = delegate (ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
                            {
                                remoteEP = remoteEndPoint;
                                return null;
                            };

                            //get the response from the request
                            req.GetResponse();
                            Log("HTTP Response: " + req.GetResponse());

                            //set remoteEndPoint to string for display
                            string hostIP = remoteEP.Address.ToString();
                            Log("Host IP: " + hostIP);

                            //stop the request to make way for a new request on next iteration
                            req.Abort();

                            //delete asteriks from sku
                            var deleteChars = new string[] { "*" };
                            foreach (var c in deleteChars)
                            {
                                linkTextBuilder = linkTextBuilder.Replace(c, string.Empty);
                            }

                            //if sku from PDF link, website response link, and pdf anchor text all match then change match
                            if (linkTextBuilder.Equals(webSite))
                            {
                                match = "MATCH";
                            }

                            //add data to datagridview
                            this.skuGrid.Rows.Add(i, null, linkTextBuilder, webSite, match, webStatus, hostIP);
                            //close http request and response
                            response.Close();

                            if (token.IsCancellationRequested)
                            {
                                // Clean up here, then...
                                token.ThrowIfCancellationRequested();
                                Log("Cancellation token: " + token);
                                MessageBox.Show("Scrape stopped");
                                throbber.Hide();
                                skuGrid.Show();
                            }
                        }

                    }
                }
                MessageBox.Show("Scrape complete");
                throbber.Hide();
                skuGrid.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log("EXCEPTION ERROR: " + ex + " " + ex.HelpLink);
                throbber.Hide();
                skuGrid.Show();
            }
            finally
            {
                taskIsRunning = false;

                //update grid
                skuGrid.Update();

                //close PDF reader
                reader.Close();

                WriteLog(log);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //on file select clear pdf filename and grid
            pdfBox.Clear();
            skuGrid.Rows.Clear();
            skuGrid.Refresh();

            //open file chooser to select file
            OpenFileDialog fileChooser = new OpenFileDialog();
            DialogResult result = fileChooser.ShowDialog();

            //make blank file type string
            string fileType = null;

            //if file is chosen, check filetype and ensure it is the right type
            if (result.ToString().Equals("OK"))
            {
                //get filetype
                fileType = "." + fileChooser.FileName.Split('.')[1];

                //if filetype does not equal pdf then force another choice
                while (!fileType.Equals(".pdf"))
                {
                    //open file chooser to select file
                    fileChooser = new OpenFileDialog();
                    result = fileChooser.ShowDialog();
                    fileType = "." + fileChooser.FileName.Split('.')[1];
                }
                //set focus to scrape button
                scrapBtn.Focus();
            }
            
            //write file name to string and append to text box to show user current pdf
            fileName = fileChooser.FileName;
            pdfBox.AppendText(fileName);
        }

        /// <summary>
        /// On click button exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Method to collect all data from datagridview and save to clipboard to export to Excel SS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateBtn_Click(object sender, EventArgs e)
        {

            //copy items to clipboard method
            if (skuGrid.Rows.Count != 0)
            {
                if (taskIsRunning == false)
                {
                    copyAlltoClipboard();

                    //instantiate workbook
                    Excel.Application xlexcel;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlexcel = new Excel.Application();
                    xlexcel.Visible = true;
                    xlWorkBook = xlexcel.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();

                    //paste copied items in workbook
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                }
                else
                {
                    MessageBox.Show("Scrape is still running, please wait");
                }
            }
            else
            {
                MessageBox.Show("There is no information to export");
            }

            
        }

        /// <summary>
        /// method to copy all items in datagridview to clipboard
        /// </summary>
        private void copyAlltoClipboard()
        {
            skuGrid.SelectAll();
            skuGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = skuGrid.GetClipboardContent();
            skuGrid.MultiSelect = true;
            skuGrid.SelectAll();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
            throbber.Hide();
            skuGrid.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Catalog Scraper v 1.3\nMade by Mike Uhrlaub");
        }

        private void Log(Object s)
        {
            log.Add(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + " | " + s + "\n");
        }

        private void WriteLog(List<Object> log)
        {
            using (StreamWriter logWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\log.txt", true))
            {
                logWriter.WriteLine("======================================================================");
                for (int i = 0; i < log.Count; i++)
                {
                    logWriter.WriteLine(log[i]);
                }
                logWriter.WriteLine("======================================================================");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
