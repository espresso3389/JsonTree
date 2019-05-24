using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public JToken Json
        {
            get { return (JToken)GetValue(JsonProperty); }
            set { SetValue(JsonProperty, value); }
        }

        public static readonly DependencyProperty JsonProperty = DependencyProperty.Register(nameof(Json), typeof(JToken), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Json = JToken.Parse(@"
{
    ""glossary"": {
        ""title"": ""example glossary"",
		""GlossDiv"": {
            ""title"": ""S"",
			""GlossList"": {
                ""GlossEntry"": {
                    ""ID"": ""SGML"",
					""SortAs"": ""SGML"",
					""GlossTerm"": ""Standard Generalized Markup Language"",
					""Acronym"": ""SGML"",
					""Abbrev"": ""ISO 8879:1986"",
					""GlossDef"": {
                        ""para"": ""A meta-markup language, used to create markup languages such as DocBook."",
						""GlossSeeAlso"": [""GML"", ""XML""]
                    },
					""GlossSee"": ""markup""
                }
            }
        }
    }
}");
            }
            else
            {
                loadJsonASync();
            }
        }

        async Task loadJsonASync()
        {
            using (var http = new HttpClient())
            {
                var res = await http.GetAsync("https://next.json-generator.com/api/json/get/NkkSKxbpL");
                res.EnsureSuccessStatusCode();
                Json = JToken.Parse(await res.Content.ReadAsStringAsync());
            }
        }
    }
}
