using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestfulClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            string post = "{ \"UserId\":\"Admi\", \"Password\":\"123456\" }";
            string content = "application/json";
            string endPoint = @"https://localhost/restSecure/RestfulService.svc/loginJson"; //10.0.2.2
            //string endPoint = @"http://localhost:12321/RestfulService.svc/loginJson"; //10.0.2.2
            var client = new RestClient(endpoint: endPoint,
                            method: HttpVerb.POST,
                            contentType: content,
                            postData: post);
            var json = client.MakeRequest();
            txtResponse.Text = json;
        }

        private void RestSharp()
        {
            //var client = new RestClient("http://localhost:12321/");
            //// client.Authenticator = new HttpBasicAuthenticator(username, password);

            //var request = new RestRequest("loginXml", Method.POST);
            //request.RequestFormat = DataFormat.Xml;
            //request.AddParameter("userId", "value"); // adds to POST or URL querystring based on Method
            //request.AddParameter("password", "123456"); // adds to POST or URL querystring based on Method
            ////request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            //// easily add HTTP Headers
            //request.AddHeader("header", "value");

            //// add files to upload (works with compatible verbs)
            ////request.AddFile(path);

            //// execute the request
            //IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string
            //txtResponse.Text = response.Content;

            //// or automatically deserialize result
            //// return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            ////RestResponse<Person> response2 = client.Execute<Person>(request);
            ////var name = response2.Data.Name;

            //// easy async support
            ////client.ExecuteAsync(request, response =>
            ////{
            ////    txtResponse.Text = response.Content;
            ////});

            //// async with deserialization
            ////var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            ////{
            ////    Console.WriteLine(response.Data.Name);
            ////});

            //// abort the request on demand
            ////asyncHandle.Abort();
        }
    }
}
