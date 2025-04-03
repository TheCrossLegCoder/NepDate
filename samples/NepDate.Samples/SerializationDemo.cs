using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NepDate.Serialization;
using Newtonsoft.Json;
using System.Text.Json;

namespace NepDate.Samples
{
    /// <summary>
    /// Demonstrates serialization and deserialization of NepaliDate using different approaches.
    /// </summary>
    public static class SerializationDemo
    {
        /// <summary>
        /// Runs the serialization demonstration.
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("=== NepaliDate Serialization Demo ===\n");
            
            // Create sample data to serialize
            var todayInBS = NepaliDate.Now;
            var sampleDate = new NepaliDate(2080, 4, 15);
            var dateList = new List<NepaliDate>
            {
                new NepaliDate(2080, 1, 1),
                new NepaliDate(2080, 4, 15),
                new NepaliDate(2080, 8, 29)
            };
            
            // Create a sample object that contains NepaliDate properties
            var sampleObject = new Person
            {
                Name = "Ram Sharma",
                BirthDate = new NepaliDate(2040, 2, 15),
                JoinDate = sampleDate
            };
            
            Console.WriteLine("Sample date: " + sampleDate);
            Console.WriteLine();
            
            // System.Text.Json Serialization (String Format)
            DemoSystemTextJson(sampleDate, dateList, sampleObject, false);
            
            // System.Text.Json Serialization (Object Format)
            DemoSystemTextJson(sampleDate, dateList, sampleObject, true);
            
            // Newtonsoft.Json Serialization (String Format)
            DemoNewtonsoftJson(sampleDate, dateList, sampleObject, false);
            
            // Newtonsoft.Json Serialization (Object Format)
            DemoNewtonsoftJson(sampleDate, dateList, sampleObject, true);
            
            // XML Serialization
            DemoXmlSerialization(sampleObject);
        }
        
        private static void DemoSystemTextJson(NepaliDate date, List<NepaliDate> dates, Person person, bool useObjectFormat)
        {
            Console.WriteLine($"=== System.Text.Json Serialization ({(useObjectFormat ? "Object" : "String")} Format) ===");
            
            // Configure serialization options
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            }.ConfigureForNepaliDate(useObjectFormat);
            
            // Serialize a single date
            string dateJson = JsonSerializer.Serialize(date, options);
            Console.WriteLine("Single date JSON:");
            Console.WriteLine(dateJson);
            Console.WriteLine();
            
            // Deserialize a single date
            var deserializedDate = JsonSerializer.Deserialize<NepaliDate>(dateJson, options);
            Console.WriteLine($"Deserialized date: {deserializedDate}");
            Console.WriteLine();
            
            // Serialize a list of dates
            string datesJson = JsonSerializer.Serialize(dates, options);
            Console.WriteLine("Date list JSON:");
            Console.WriteLine(datesJson);
            Console.WriteLine();
            
            // Serialize an object with date properties
            string personJson = JsonSerializer.Serialize(person, options);
            Console.WriteLine("Person object JSON:");
            Console.WriteLine(personJson);
            Console.WriteLine();
            
            // Deserialize the object
            var deserializedPerson = JsonSerializer.Deserialize<Person>(personJson, options);
            Console.WriteLine($"Deserialized person: {deserializedPerson.Name}, Birth: {deserializedPerson.BirthDate}, Join: {deserializedPerson.JoinDate}");
            Console.WriteLine();
        }
        
        private static void DemoNewtonsoftJson(NepaliDate date, List<NepaliDate> dates, Person person, bool useObjectFormat)
        {
            Console.WriteLine($"=== Newtonsoft.Json Serialization ({(useObjectFormat ? "Object" : "String")} Format) ===");
            
            // Configure serialization settings
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            }.ConfigureForNepaliDate(useObjectFormat);
            
            // Serialize a single date
            string dateJson = JsonConvert.SerializeObject(date, settings);
            Console.WriteLine("Single date JSON:");
            Console.WriteLine(dateJson);
            Console.WriteLine();
            
            // Deserialize a single date
            var deserializedDate = JsonConvert.DeserializeObject<NepaliDate>(dateJson, settings);
            Console.WriteLine($"Deserialized date: {deserializedDate}");
            Console.WriteLine();
            
            // Serialize a list of dates
            string datesJson = JsonConvert.SerializeObject(dates, settings);
            Console.WriteLine("Date list JSON:");
            Console.WriteLine(datesJson);
            Console.WriteLine();
            
            // Serialize an object with date properties
            string personJson = JsonConvert.SerializeObject(person, settings);
            Console.WriteLine("Person object JSON:");
            Console.WriteLine(personJson);
            Console.WriteLine();
            
            // Deserialize the object
            var deserializedPerson = JsonConvert.DeserializeObject<Person>(personJson, settings);
            Console.WriteLine($"Deserialized person: {deserializedPerson.Name}, Birth: {deserializedPerson.BirthDate}, Join: {deserializedPerson.JoinDate}");
            Console.WriteLine();
        }
        
        private static void DemoXmlSerialization(Person person)
        {
            Console.WriteLine("=== XML Serialization ===");
            
            var serializer = new XmlSerializer(typeof(PersonXml));
            var personXml = new PersonXml(person);
            
            // Serialize to XML
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
            {
                serializer.Serialize(xmlWriter, personXml);
            }
            
            string xml = stringWriter.ToString();
            Console.WriteLine("Person XML:");
            Console.WriteLine(xml);
            Console.WriteLine();
            
            // Deserialize from XML
            var stringReader = new StringReader(xml);
            var deserializedPersonXml = (PersonXml)serializer.Deserialize(stringReader);
            var deserializedPerson = deserializedPersonXml.ToPerson();
            
            Console.WriteLine($"Deserialized person: {deserializedPerson.Name}, Birth: {deserializedPerson.BirthDate}, Join: {deserializedPerson.JoinDate}");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Sample class with NepaliDate properties.
        /// </summary>
        public class Person
        {
            public string Name { get; set; }
            public NepaliDate BirthDate { get; set; }
            public NepaliDate JoinDate { get; set; }
        }
        
        /// <summary>
        /// XML-serializable version of the Person class.
        /// </summary>
        public class PersonXml
        {
            public PersonXml()
            {
            }
            
            public PersonXml(Person person)
            {
                Name = person.Name;
                BirthDate = new NepaliDateXmlSerializer(person.BirthDate);
                JoinDate = new NepaliDateXmlSerializer(person.JoinDate);
            }
            
            public string Name { get; set; }
            
            public NepaliDateXmlSerializer BirthDate { get; set; }
            
            public NepaliDateXmlSerializer JoinDate { get; set; }
            
            public Person ToPerson()
            {
                return new Person
                {
                    Name = Name,
                    BirthDate = BirthDate.Value,
                    JoinDate = JoinDate.Value
                };
            }
        }
    }
} 