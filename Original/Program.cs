using System;
using System.IO;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace Original {
	internal class Program {
		private const string Filename = "Test.rules.xml";

		static void Main() {
			RuleSet ruleset = null;

			// Obtain or create ruleset
			if (File.Exists(Filename)) {
				// load file
				ruleset = Load(Filename);
			} else {
				ruleset = new RuleSet();
			}

			RuleSetDialog dialog = new RuleSetDialog(typeof(Person), null, ruleset);
			DialogResult result = dialog.ShowDialog();

			if (result == DialogResult.OK) {
				// save the file
				Save(Filename, dialog.RuleSet);
			}
		}

		static RuleSet Load(string filename) {
			XmlTextReader reader = new XmlTextReader(filename);
			WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
			object results = serializer.Deserialize(reader);
			RuleSet ruleset = (RuleSet)results;

			if (ruleset == null) {
				Console.WriteLine("The rules file " + filename + " does not appear to contain a valid ruleset.");
			}
			return ruleset;
		}

		static void Save(string filename, RuleSet ruleset) {
			XmlTextWriter writer = new XmlTextWriter(filename, null);
			WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
			serializer.Serialize(writer, ruleset);
			Console.WriteLine("Wrote rules file: " + filename);
		}

	}
}