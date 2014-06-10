using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Samples.Wpf.Model;

namespace Samples.Wpf.Services
{
    public class SampleService : ISampleService
    {
        public IEnumerable<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person
                {
                    Picture = GetPicture("Person.png"),
                    Name = "Ian",
                    Age = 29,
                    OwnedHouses = new List<House>
                    {
                        new House
                        {
                            Surface = 300,
                            Location = "Denver"
                        }
                    }
                },
                new Person
                {
                    Picture = GetPicture("Person.png"),
                    Name = "JMN",
                    Age = 33,
                    OwnedHouses = new List<House>
                    {
                        new House
                        {
                            Surface = 80,
                            Location = "Madrid"
                        },
                          new House
                        {
                            Surface = 130,
                            Location = "Ciudad Real"
                        }
                    }
                },
            };
        }

        private ImageSource GetPicture(string pictureName)
        {
            var assemblyName = GetType().Assembly.GetName().Name;
            var path = String.Format("pack://application:,,,/{0};component/Images/{1}", assemblyName, pictureName);
            var uri = new Uri(path, UriKind.Absolute);
            // ReSharper disable once MaximumChainedReferences
            // ReSharper disable once MaximumChainedReferences
            var source = new BitmapImage(uri);
            return source;
        }
    }

     public static class WPFPackUriHelper
    {
        /// <summary>
        /// Constructs a pack Uri for a resource in 'local assembly'.
        /// </summary>
        /// <param name="path">Path to local assembly resource</param>
        /// <returns>Pack Uri for a resource in 'local assembly'</returns>
        /// <remarks>
        /// Based on rules enumerated at http://msdn.microsoft.com/en-us/library/aa970069(VS.85).aspx#Resource_File_Pack_URIs___Local_Assembly
        /// </remarks>
        public static Uri CreateLocalAssemblyPackUri(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
 
            return Create(_applicationAuthority, path);
        }
 
        /// <summary>
        /// Constructs a pack Uri for a resource in a referenced assembly.
        /// </summary>
        /// <param name="name">Name of referenced assembly</param>
        /// <param name="path">Path to referenced assembly resource</param>
        /// <returns>Pack Uri for a resource in referenced assembly</returns>
        /// <remarks>
        /// Based on rules enumerated at http://msdn.microsoft.com/en-us/library/aa970069(VS.85).aspx#Resource_File_Pack_URIs___Referenced_Assembly
        /// </remarks>
        public static Uri CreateReferencedAssemblyPackUri(AssemblyName name, string path)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
 
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
 
            string assemblyShortName = name.Name;
            if (string.IsNullOrWhiteSpace(assemblyShortName))
            {
                throw new ArgumentException("Argument must not have null or empty name", "name");
            }
 
            const int averageVersionStringLength = 10; // 4 dot separators + 4 digits + 2 extra padding
            const int averagePublicKeyTokenLength = 16; // 8 hex digits (2 characters per digit)
            int averageAssemblySpecifierLength = assemblyShortName.Length + averageVersionStringLength + averagePublicKeyTokenLength;
 
            StringBuilder assemblySpecifier = new StringBuilder(averageAssemblySpecifierLength);
            assemblySpecifier.Append(assemblyShortName);
 
            Version version = name.Version;
            if(version != null)
            {
                assemblySpecifier.Append(';');
                assemblySpecifier.Append(version.ToString());
            }
 
            byte[] publicKeyToken = name.GetPublicKeyToken();
            if (publicKeyToken != null)
            {
                assemblySpecifier.Append(';');
                assemblySpecifier.Append(ToHexString(publicKeyToken, 0, publicKeyToken.Length));
            }
 
            assemblySpecifier.Append(";component");
 
            Uri packageUri = new Uri(_applicationAuthority, assemblySpecifier.ToString());
 
            return Create(packageUri, path);
        }
 
        /// <summary>
        /// Constructs a pack Uri for a 'content file' resource.
        /// </summary>
        /// <param name="path">Path to content file resource</param>
        /// <returns>Pack Uri for a content file resource</returns>
        /// <remarks>
        /// Based on rules enumerated at http://msdn.microsoft.com/en-us/library/aa970069(VS.85).aspx#Content_File_Pack_URIs
        /// </remarks>
        public static Uri CreateContentFilePackUri(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
 
            return Create(_applicationAuthority, path);
        }
 
        /// <summary>
        /// Constructs a pack Uri for a 'site of origin' resource.
        /// </summary>
        /// <param name="path">Path to site of origin resource</param>
        /// <returns>Pack Uri for a site of origin resource</returns>
        /// <remarks>
        /// Based on rules enumerated at http://msdn.microsoft.com/en-us/library/aa970069(VS.85).aspx#Site_of_Origin_File_Pack_URIs
        /// </remarks>
        public static Uri CreateSiteOfOriginPackUri(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
 
            return Create(_siteOfOriginAuthority, path);
        }
 
        private static Uri Create(Uri packageUri, string path)
        {
            return PackUriHelper.Create(packageUri, PackUriHelper.CreatePartUri(new Uri(path, UriKind.Relative)));
        }
 
        /// <summary>
        /// Converts a byte array to a hexidecimal string
        /// </summary>
        /// <param name="bytes">input byte sequence</param>
        /// <returns>A hexidecimal string</returns>
        /// <remarks>
        /// I can't believe I have to roll out my own converter but 
        /// the closest equivalent 'BitConvert.ToString(byte [])' pads the bytes with dashes
        /// </remarks>
        private static string ToHexString(byte[] bytes, int startIndex, int count)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
 
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
 
            if (startIndex >= bytes.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
 
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
 
            int endIndex = startIndex + count;
            if (endIndex > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("count");
            }
 
            StringBuilder result = new StringBuilder(count * 2); // two characters per byte
            for (int i = startIndex; i < endIndex; i++)
            {
                byte b = bytes[i];
 
                result.Append(ToHexDigit(b >> 4));
                result.Append(ToHexDigit(b & 0x0F));
            }
 
            return result.ToString();
        }
 
        private static char ToHexDigit(int value)
        {
            Debug.Assert(value >= 0);
            Debug.Assert(value < 0x10);
 
            return (value < 0x0A) ? ((char)('0' + value)) : ((char)('a' + (value - 0x0A)));
        }
 
        private static readonly Uri _applicationAuthority = new Uri("application:///.", UriKind.Absolute);
        private static readonly Uri _siteOfOriginAuthority = new Uri("siteoforigin:///.", UriKind.Absolute);
    }
}

