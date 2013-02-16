using System;
using System.IO;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace Original {
	class Program
	{
		private const string Filename = "Test.rules.xml";

		static void Main() {
			RuleSet ruleset = null;

			Person person = new Person {Age = 70};

			if (File.Exists(Filename)) {
				// load file
				ruleset = Load(Filename);
			}

			if (ruleset != null) {
				RuleValidation validation = new RuleValidation(person.GetType(), null);
				RuleExecution engine = new RuleExecution(validation, person);
				ruleset.Execute(engine);
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
	}
}