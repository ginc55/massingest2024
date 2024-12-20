using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.Common;
using System.Windows.Controls;
using System.Xml;
using DomObjectImport.WorkClas.HelperObjects;

namespace DomObjectImport.WorkClas
{

    public class WebClientEx : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = Timeout;
            return request;
        }
    }

    class ProcesClass
    {



        public static void AddDomObject()
        {
            int kopejie_objekti = 0;
            int OK_objekti = 0;
            int false_objekti = 0;
            int importetie_objekti = 0;


            StartAddDigitalObjectRequest domObjectVariables = new StartAddDigitalObjectRequest();
            domObjectVariables.Fields = new List<DigitalObjectMetadataField>();
            StartAddDigitalObjectResponse domMetadataAddResponse = new StartAddDigitalObjectResponse();
            domMetadataAddResponse.Errors = new List<string>();
            CompleteAddObjectResponse UploadFinishResponse = new CompleteAddObjectResponse();
            UploadFinishResponse.Errors = new List<string>();

            DigitalObjectMetadataField DigitalObjectMetadata = new DigitalObjectMetadataField();
            DigitalObjectMetadata.Subfields = new List<DigitalObjectMetadataSubfield>();
            List<DigitalObjectMetadataSubfield> DigitalObjectMetadataSubfieldList = new List<DigitalObjectMetadataSubfield>();


            List<DigitalObjectMetadataSubfield> _tempListSubfields = new List<DigitalObjectMetadataSubfield>();
            List<XmlDocument> xmlRedyLists = new List<XmlDocument>();


            DataTable DatiXml = excelvertibas.exceltabula;
            excelvertibas.errorlist = new List<string>();
            excelvertibas.getexternalid = new List<string>();
            

            List<string> ExcelcolumnLists = new List<string>();
            List<string> creatorLists = new List<string>();
            List<string> thesaurusCreatorLists = new List<string>();
            List<string> subjectLists = new List<string>();
            List<string> thesaurusSubjectLists = new List<string>();
            List<string> isPartOfCollectionLists = new List<string>();
            List<string> isPartOfLists = new List<string>();
            List<string> abtracttLists = new List<string>();
            List<string> abtract_languageLists = new List<string>();
            List<string> isPartOfSerieLists = new List<string>();
            List<string> isPartOfSerie_thesaurusLists = new List<string>();
            List<string> geographicNameLists = new List<string>();
            List<string> geographicName_thesaurusLists = new List<string>();
            List<string> contributor_Lists = new List<string>();
            List<string> contributor_thesaurusLists = new List<string>();
            List<string> contributorroleLists = new List<string>();
            List<string> relationLists = new List<string>();
            List<string> isVersionOfLists = new List<string>();
            List<string> hasVersionLists = new List<string>();
            List<string> isReplacedByLists = new List<string>();
            List<string> replacesLists = new List<string>();
            List<string> isRequiredByLists = new List<string>();
            List<string> requiresLists = new List<string>();
            List<string> hasPartLists = new List<string>();
            List<string> isReferencedByLists = new List<string>();
            List<string> referencesLists = new List<string>();
            List<string> isFormatOfLists = new List<string>();
            List<string> hasFormatLists = new List<string>();
            List<string> conformsToLists = new List<string>();
            List<string> relationOfLists = new List<string>();
            List<string> collectionPartLists = new List<string>();

            List<string> titleLists = new List<string>();
            List<string> title_languageLists = new List<string>();
            List<string> alternativeTitleLists = new List<string>();
            List<string> alternativeTitle_languageLists = new List<string>();
            List<string> formatExtentLists = new List<string>();
            List<string> formatExtent_languageLists = new List<string>();
            List<string> descriptionLists = new List<string>();
            List<string> description_languageLists = new List<string>();
            List<string> tableOfContentsLists = new List<string>();
            List<string> tableOfContents_languageLists = new List<string>();

            List<string> sourceLists = new List<string>();
            List<string> source_languageLists = new List<string>();

            List<string> nonRepeatableElements = new List<string> { "dateCreated",
                                                    "formatmedium", "uri","isShownAt","thumbnail","locationOrganization","locationDepartment","boxBoundingCoordinates","volume","issue","dateIssued",
                                                    "alephId","publicationFrequency","boxBoundingCoordinatesE","boxBoundingCoordinatesN","boxBoundingCoordinatesS","scientificWorkType", "competentInstitution",
                                                    "competentPerson"}; //neatkartojamie elementi
            
            

            List<string> RepeatableElements = new List<string> { "edition", "formatExtent", "alternativeTitle" , "publisher", "keyword", "abstract", "tableOfContents" , "description", "origin", "dedication",
                                                                 "scientificComment","isbn","issn","ismn","identifier","locationCode","language","coverageTemporal","geographicName","place","address",
                                                                 "scale","projection","point","frameOfReference","source","relation","isVersionOf","hasVersion","isReplacedBy","replaces","isRequiredBy","replaces",
                                                                 "isRequiredBy","requires","isPartOf","hasPart","isReferencedBy","references","isFormatOf","hasFormat","conformsTo","isPartOfCollection","isPartOfSerie",
                                                                 "year","url","sameAsUri","dataProvider","topic","audience","relationOf","collectionPart","subject","contributor" }; // atkartojamie elementi
            

            List<string> ThesaurusTypeRepeatableElements = new List<string>(); // tezaurs pie atkarojamiem elementiem
            ThesaurusTypeRepeatableElements.Add("contributor_thesaurus");

            List<string> RepeatableElementsThesaurus = new List<string>();
            RepeatableElementsThesaurus.Add("subject");
            RepeatableElementsThesaurus.Add("isPartOfSerie");
            RepeatableElementsThesaurus.Add("geographicName");
            
           

            List<string> MultiLangStringList = new List<string>();
            MultiLangStringList.Add("source");
            MultiLangStringList.Add("scientificComment");
            MultiLangStringList.Add("description");
            MultiLangStringList.Add("tableOfContents");
            MultiLangStringList.Add("abstract");
            MultiLangStringList.Add("alternativeTitle");
            MultiLangStringList.Add("formatExtent");

            foreach (DataColumn c in DatiXml.Columns)  //loop through columns, and get full colum list
            {
                if (!string.IsNullOrWhiteSpace(c.ColumnName.ToString()))
                {
                    ExcelcolumnLists.Add(c.ColumnName.ToString());

                }
            }

            for (var i = 0; i < DatiXml.Rows.Count; i++)//iziet cauri objektu rindam
            {
                
                #region progressbar
                float fTemp = 0f;
                float progress = 0f;

                if (xmlRedyLists.Count() > 1)
                {
                    fTemp = (xmlRedyLists.Count());
                    float TempNumbber = 100F / fTemp;

                    if (i == 0)
                    {
                        progress = TempNumbber;
                    }
                    else if (i > 0)
                    {
                        progress = (i + 1) * TempNumbber;
                    }
                }
                else
                {
                    progress = 100f;
                }
                #endregion progressbar

                var tests = "";
                for (var kk = 0; kk < DatiXml.Rows[i].ItemArray.Count(); kk++)//skatas vai ir tuksas rindas
                {
                    tests += DatiXml.Rows[i].ItemArray[kk].ToString();
                }
                if (!string.IsNullOrWhiteSpace(tests))
                {
                    List<string> sourceLanguageList = new List<string>();
                    List<string> scientificCommentLanguageList = new List<string>();
                    List<string> descriptionLanguageList = new List<string>();
                    List<string> tableOfContentsLanguageList = new List<string>();
                    List<string> abstractLanguageList = new List<string>();
                    List<string> alternativeTitleLanguageList = new List<string>();
                    List<string> formatExtentLanguageList = new List<string>();

                    #region getting languages
                    for (var z = 0; z < ExcelcolumnLists.Count; z++) // getting language values
                    {
                        var cellValue = DatiXml.Rows[i][ExcelcolumnLists[z]];

                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "source", sourceLanguageList, cellValue.ToString());
                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "scientificComment", scientificCommentLanguageList, cellValue.ToString());
                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "description", descriptionLanguageList, cellValue.ToString());
                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "tableOfContents", tableOfContentsLanguageList, cellValue.ToString());
                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "abstract", abstractLanguageList, cellValue.ToString());
                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "alternativeTitle", alternativeTitleLanguageList, cellValue.ToString());
                        HelperC.ColectAllLanguageFieldValues(ExcelcolumnLists[z], "formatExtent", formatExtentLanguageList, cellValue.ToString());

                    }
                    #endregion getting languages

                    IndexObjectOfLanguageList tempIndexListO = new IndexObjectOfLanguageList();
                    tempIndexListO.sourceInt = 0;
                    tempIndexListO.scientificCommentInt = 0;
                    tempIndexListO.descriptionInt = 0;
                    tempIndexListO.tableOfContentsInt = 0;
                    tempIndexListO.abstractInt = 0;
                    tempIndexListO.alternativeTitleInt = 0;
                    tempIndexListO.formatExtentInt = 0;

                    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
                    for (var z = 0; z < ExcelcolumnLists.Count; z++) //lai savaktu informaciju no galvenem, ja tam ir vienadi nosaukumi
                    {
                        var cellValue = DatiXml.Rows[i][ExcelcolumnLists[z]]; //content of column, value

                        //-------------------------------------------------------------------------------------------saliek galvenas vertibas
                        #region main values
                        if (ExcelcolumnLists[z].Trim().ToLower() == "subtype")
                        {
                            domObjectVariables.Type = cellValue.ToString();

                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "title")
                        {
                            domObjectVariables.Title = cellValue.ToString();
                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "title_language")
                        {
                            domObjectVariables.TitleLang = cellValue.ToString();
                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "importsource")
                        {
                            domObjectVariables.Source = cellValue.ToString();
                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "externalid")
                        {
                            domObjectVariables.ExternalId = cellValue.ToString();
                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "type")
                        {
                            domObjectVariables.subType = cellValue.ToString();
                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "copyright")
                        {
                            domObjectVariables.Copyright = cellValue.ToString();
                        }
                        if (ExcelcolumnLists[z].Trim().ToLower() == "accessright")
                        {
                            domObjectVariables.AccessRight = cellValue.ToString();
                        }
                        #endregion main values
                        //----------------------------------------------------------------------------------------------pievieno neatkartojamos elementus
                        #region adding nonRepeatable Elements
                        foreach (var ff in nonRepeatableElements)
                        {
                            if (string.Equals(ExcelcolumnLists[z], ff, StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrWhiteSpace(cellValue.ToString()))
                                {
                                    domObjectVariables.Fields.Add(new DigitalObjectMetadataField()
                                    {
                                        ElementName = ff,
                                        Value = cellValue.ToString()
                                        

                                    });
                                }
                            }
                        }
                        #endregion adding nonRepeatable Elements
                        //--------------------------------------------------------------------------------------------pievieno atkartojamos elementus un valodu ja vajag

                        LanguageListO languageListObject = new LanguageListO();

                        #region adding Repeatable Elements
                        

                        foreach (var ff in RepeatableElements)//loop through all Repeatable Elements
                        {
                            if (string.Equals(HelperC.TrimNumbers(ExcelcolumnLists[z]), ff, StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrWhiteSpace(cellValue.ToString()))
                                {
                                    bool hasValue = false;

                                    foreach (var gg in MultiLangStringList)//if this element is multilang add it to object
                                    {
                                        List<string> tempList = new List<string>();

                                        if (string.Equals(HelperC.TrimNumbers(ExcelcolumnLists[z]), gg, StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (ff == "contributor")
                                            {
                                                languageListObject = HelperC.GettingCorectLanguageList(gg, sourceLanguageList, scientificCommentLanguageList, descriptionLanguageList, tableOfContentsLanguageList, abstractLanguageList, alternativeTitleLanguageList, formatExtentLanguageList, tempIndexListO);

                                                domObjectVariables.Fields.Add(new DigitalObjectMetadataField()
                                                {
                                                    ElementName = ff,
                                                    Value = cellValue.ToString(),
                                                    Lang = languageListObject.tempLanguageList[languageListObject.indexInList],
                                                    ThesaurusType = DatiXml.Rows[i].Field<string>("contributor_thesaurus")





                                                });
                                            }
                                            else
                                            {
                                                languageListObject = HelperC.GettingCorectLanguageList(gg, sourceLanguageList, scientificCommentLanguageList, descriptionLanguageList, tableOfContentsLanguageList, abstractLanguageList, alternativeTitleLanguageList, formatExtentLanguageList, tempIndexListO);

                                                domObjectVariables.Fields.Add(new DigitalObjectMetadataField()
                                                {
                                                    ElementName = ff,
                                                    Value = cellValue.ToString(),
                                                    Lang = languageListObject.tempLanguageList[languageListObject.indexInList]
                                                    





                                                });
                                            }
                                            hasValue = true;
                                        }
                                    }
                                    if (hasValue == false)//ja valodas elementam nav atributs
                                    {
                                        if (ff == "contributor")
                                        {


                                            if (DatiXml.Rows[i].Field<string>("contributorRole") != null)
                                            { 
                                                _tempListSubfields.Add(new DigitalObjectMetadataSubfield()
                                                {
                                                    ElementName = "role",
                                                    Value = DatiXml.Rows[i].Field<string>("contributorRole").Trim()
                                                });
                                            }
                                            domObjectVariables.Fields.Add(new DigitalObjectMetadataField()
                                            {
                                                ElementName = ff,
                                                Value = cellValue.ToString(),
                                                ThesaurusType = DatiXml.Rows[i].Field<string>("contributor_thesaurus").Trim(),
                                                Subfields = _tempListSubfields

                                            }) ;
                                        }
                                        else
                                        {
                                            domObjectVariables.Fields.Add(new DigitalObjectMetadataField()
                                            {
                                                ElementName = ff,
                                                Value = cellValue.ToString()
                                                

                                            });

                                        }
                                    }
                                }

                                tempIndexListO.sourceInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "source");// for setting correct item in list
                                tempIndexListO.scientificCommentInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "scientificComment");
                                tempIndexListO.descriptionInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "description");
                                tempIndexListO.tableOfContentsInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "tableOfContents");
                                tempIndexListO.abstractInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "abstract");
                                tempIndexListO.alternativeTitleInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "alternativeTitle");
                                tempIndexListO.formatExtentInt = HelperC.GetIndexOfListItem(ExcelcolumnLists[z], "formatExtent");
                            }
                        }
                        #endregion adding Repeatable Elements

                        //--------------------------------------------------------------------------------------------------------------------------------------------------------------




                        

                    }
                        domObjectVariables.VerifyOnly = excelvertibas.verify_only;
                        
                    using (var client = new WebClientEx())//digitala objekta uzsaksana
                    {
                        client.BaseAddress = serverurl.serviceurl;
                        client.UseDefaultCredentials = true;
                        string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(excelvertibas.username + ":" + excelvertibas.password));
                        client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        client.Timeout = 90000;

                        byte[] postArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(domObjectVariables));
                        client.Headers.Add("Content-Type", "application/json");//----------------
                        client.Encoding = Encoding.UTF8;
                        byte[] responseArray = null;
                        string json = JsonConvert.SerializeObject(domObjectVariables);
                        
                        try
                       {
                            responseArray = client.UploadData(serverurl.serviceurl + "StartAddDigitalObject", "post", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domObjectVariables)));
                        }
                        catch(WebException)
                       {
                           MessageBox.Show("500 errors savienojoties ar serveri StartAddDigitalObject servisa linkā");
                           return;
                       }
                        //----------------------------
                        domMetadataAddResponse = JsonConvert.DeserializeObject<StartAddDigitalObjectResponse>(System.Text.Encoding.UTF8.GetString(responseArray));


                        kopejie_objekti++;
                    }
                    
                    if (domObjectVariables.VerifyOnly == true)
                    {
                        if (domMetadataAddResponse.Errors != null)
                        {
                            false_objekti++;
                            excelvertibas.getexternalid.Add(domObjectVariables.ExternalId);
                           

                        }
                        else
                        {
                            OK_objekti++;
                               
                        }
                        

                        domObjectVariables.Fields.Clear();
                        _tempListSubfields.Clear();
                        
                    }
                    if (domObjectVariables.VerifyOnly == false)
                    {
                        if (domMetadataAddResponse.Errors != null)// ja ir erroru , tad pievieno sarakstam un pieskaita pie objektiem ar kludam
                        {
                            false_objekti++;
                            excelvertibas.getexternalid.Add(domObjectVariables.ExternalId);
                        }
                        else
                        {
                            OK_objekti++;
                        }

                    }

                        string responseStringError6 = "";
                    if (domMetadataAddResponse.Errors != null)
                    {
                        foreach (var m in domMetadataAddResponse.Errors)
                        {
                            excelvertibas.errorlist.Add(domObjectVariables.ExternalId + " errori: \n");
                            responseStringError6 += m.ToString();
                            excelvertibas.errorlist.Add(m.ToString());
                            excelvertibas.errorlist.Add("------------------------------");

                        }
                        if(domObjectVariables.VerifyOnly == false)
                        {
                            false_objekti++;
                            
                        }
                        
                    }
                    

                    if (domMetadataAddResponse.Errors == null)
                    {
                        if (domMetadataAddResponse.OK == true)
                        {
                            if (domMetadataAddResponse.DigitalObjectId != null)
                            {
                                StartChunkedUploadResponse fileaddresponse = new StartChunkedUploadResponse();
                                StartAddFileToDigitalObjectRequest domFileVariables = new StartAddFileToDigitalObjectRequest();
                                string file_access = "file_Access";
                                try
                                {
                                    if (DatiXml.Rows[i].Field<string>(file_access) != null)
                                    {
                                        if (File.Exists(System.IO.Path.Combine(excelvertibas.SourcePath, DatiXml.Rows[i].Field<string>(file_access))) == true)
                                        {


                                            int ii = 1;
                                            if (DatiXml.Rows[i].Table.Columns.Contains(file_access))
                                            {
                                                while (DatiXml.Rows[i].Field<string>(file_access) != null)//uzsakt failu pievienosanu
                                                {

                                                    domFileVariables.DigitalObjectId = (int)domMetadataAddResponse.DigitalObjectId;
                                                    domFileVariables.FileName = DatiXml.Rows[i].Field<string>(file_access);
                                                    domFileVariables.Type = 1;
                                                    string sourceFile = System.IO.Path.Combine(excelvertibas.SourcePath, DatiXml.Rows[i].Field<string>(file_access));

                                                    domFileVariables.DisplayName = "";
                                                    domFileVariables.Description = "";
                                                    domFileVariables.Size = new System.IO.FileInfo(sourceFile).Length;
                                                    domFileVariables.Hash = sendfiles.CalculateMD5(sourceFile);


                                                    using (var client = new WebClientEx())
                                                    {

                                                        client.BaseAddress = serverurl.serviceurl;
                                                        client.UseDefaultCredentials = true;
                                                        string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(excelvertibas.username + ":" + excelvertibas.password));
                                                        client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                                                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                                                        client.Timeout = 90000;


                                                        byte[] postArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(domFileVariables));
                                                        client.Headers.Add("Content-Type", "application/json");//----------------
                                                        client.Encoding = Encoding.UTF8;
                                                        byte[] responseArray = client.UploadData(serverurl.serviceurl + "StartAddFileToNewDigitalObject", "post", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domFileVariables)));  //----------------------------
                                                        fileaddresponse = JsonConvert.DeserializeObject<StartChunkedUploadResponse>(Encoding.UTF8.GetString(responseArray));

                                                    }
                                                    string responseStringError4 = "";
                                                    if (fileaddresponse.OK == false)
                                                    {
                                                        excelvertibas.errorlist.Add("Failu priekš objekta " + domObjectVariables.ExternalId + " nevarēja uzsākt pievienošanu(skatīt outputlistā detalizētāk)\n");

                                                    }
                                                    if (fileaddresponse.Errors != null)
                                                    {
                                                        foreach (var m in fileaddresponse.Errors)
                                                        {
                                                            responseStringError4 += m.ToString();
                                                            excelvertibas.errorlist.Add(m.ToString());
                                                        }

                                                    }


                                                    List<string> DataFilePathList = new List<string>();
                                                    var filename = sourceFile;
                                                    DataFilePathList.Add(System.IO.Path.GetFullPath(filename));

                                                    FileStream fileStream = new FileStream(DataFilePathList[0], FileMode.Open, FileAccess.Read);
                                                    long chunkSize = 0;
                                                    chunkSize = fileaddresponse.ChunkSize;
                                                    long Size = 0;
                                                    string Hash = "";
                                                    int totalFiles = DataFilePathList.Count();
                                                    try
                                                    {
                                                        Size = new System.IO.FileInfo(filename).Length;
                                                    }
                                                    catch (FileNotFoundException)
                                                    {
                                                        MessageBox.Show("Nav atrasts fails , ko pievienot objektam");
                                                    }

                                                    Hash = sendfiles.CalculateMD5(filename);
                                                    int totalChunks = 0;
                                                    totalChunks = (int)Math.Ceiling((double)fileStream.Length / chunkSize);

                                                    int i2 = 1;
                                                    while (i2 <= totalChunks)
                                                    {
                                                        long startIndex = ((long)i2 * chunkSize) - chunkSize;
                                                        long endIndex = (startIndex + chunkSize > fileStream.Length ? fileStream.Length : startIndex + chunkSize);
                                                        long length = endIndex - startIndex;

                                                        byte[] bytes = new byte[length];
                                                        fileStream.Read(bytes, 0, bytes.Length);


                                                        string baseAddress = serverurl.serviceurl;
                                                        string errorString = sendfiles.ChunkRequest(i2, bytes, domMetadataAddResponse.DigitalObjectId, excelvertibas.username, excelvertibas.password, baseAddress, fileaddresponse.FileId); // faila nosutisanas funkcija pa gabaliem

                                                        if (errorString == "")
                                                        {
                                                            i2++;

                                                            float procenti = ((float)i2 - 1) / (float)totalChunks * 100;



                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Kļūda - " + errorString + " Tiks veikts atkārtots mēģinājums nosūtīt datus.");
                                                        }

                                                    }

                                                    SimpleResponse fileuplcheck = new SimpleResponse();

                                                    using (var client = new WebClientEx())//pabeigt pievienot failu
                                                    {

                                                        client.BaseAddress = serverurl.serviceurl;
                                                        client.UseDefaultCredentials = true;

                                                        string credentials1 = Convert.ToBase64String(Encoding.ASCII.GetBytes(excelvertibas.username + ":" + excelvertibas.password));
                                                        client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials1;
                                                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                                                        client.Headers.Add("Content-Type", "application/json");
                                                        client.Encoding = Encoding.UTF8;
                                                        client.Timeout = 20000;
                                                        byte[] responseArrayAA = client.UploadData(serverurl.serviceurl + "CompleteAddFileToNewDigitalObject", "post", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(fileaddresponse.FileId.ToString())));
                                                        fileuplcheck = JsonConvert.DeserializeObject<SimpleResponse>(Encoding.UTF8.GetString(responseArrayAA));
                                                    }
                                                    string responseStringError3 = "";
                                                    if (fileuplcheck.OK == false)
                                                    {
                                                        excelvertibas.errorlist.Add("Failu priekš objekta " + domObjectVariables.ExternalId + " neizdevās pievienot\n");

                                                    }
                                                    if (fileuplcheck.Errors != null)
                                                    {
                                                        foreach (var m in fileuplcheck.Errors)
                                                        {
                                                            responseStringError3 += m.ToString();
                                                            excelvertibas.errorlist.Add(m.ToString());
                                                        }

                                                    }




                                                    file_access = "file_Access";
                                                    file_access = file_access + "_" + ii;
                                                    ii++;

                                                    if (DatiXml.Rows[i].Table.Columns.Contains(file_access))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }

                                                }
                                            }

                                        }
                                    }
                                }
                                catch (System.ArgumentNullException)
                                {
                                    MessageBox.Show("Netika atrasts fails lai pievienotu");
                                    continue;
                                }
                                string file_archive = "file_Archive";
                                int iii = 1;
                                if (DatiXml.Rows[i].Field<string>(file_archive) != null)

                                {
                                    try
                                    {
                                        if (File.Exists(System.IO.Path.Combine(excelvertibas.SourcePath, DatiXml.Rows[i].Field<string>(file_archive))) == true)
                                        {
                                            if (DatiXml.Rows[i].Table.Columns.Contains(file_archive))
                                            {
                                                while (DatiXml.Rows[i].Field<string>(file_archive) != null)
                                                {

                                                    domFileVariables.DigitalObjectId = (int)domMetadataAddResponse.DigitalObjectId;
                                                    domFileVariables.FileName = DatiXml.Rows[i].Field<string>(file_archive);
                                                    domFileVariables.Type = 2;
                                                    string sourceFile = System.IO.Path.Combine(excelvertibas.SourcePath, DatiXml.Rows[i].Field<string>(file_archive));

                                                    domFileVariables.DisplayName = "";
                                                    domFileVariables.Description = "";
                                                    domFileVariables.Size = new System.IO.FileInfo(sourceFile).Length;
                                                    domFileVariables.Hash = sendfiles.CalculateMD5(sourceFile);


                                                    using (var client = new WebClientEx())
                                                    {

                                                        client.BaseAddress = serverurl.serviceurl;
                                                        client.UseDefaultCredentials = true;
                                                        string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(excelvertibas.username + ":" + excelvertibas.password));
                                                        client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                                                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                                                        client.Timeout = 90000;


                                                        byte[] postArray = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(domFileVariables));
                                                        client.Headers.Add("Content-Type", "application/json");//----------------
                                                        client.Encoding = Encoding.UTF8;
                                                        byte[] responseArray = client.UploadData(serverurl.serviceurl + "StartAddFileToNewDigitalObject", "post", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domFileVariables)));  //----------------------------
                                                        fileaddresponse = JsonConvert.DeserializeObject<StartChunkedUploadResponse>(Encoding.UTF8.GetString(responseArray));

                                                    }
                                                    string responseStringError4 = "";
                                                    if (fileaddresponse.OK == false)
                                                    {
                                                        excelvertibas.errorlist.Add("Failu priekš objekta " + domObjectVariables.ExternalId + " nevarēja uzsākt pievienošanu(skatīt outputlistā detalizētāk)\n");

                                                    }
                                                    if (fileaddresponse.Errors != null)
                                                    {
                                                        foreach (var m in fileaddresponse.Errors)
                                                        {
                                                            responseStringError4 += m.ToString();
                                                            excelvertibas.errorlist.Add(m.ToString());
                                                        }

                                                    }
                                                    if (!string.IsNullOrWhiteSpace(responseStringError4))
                                                    {
                                                        //outputlist.errorlist.Text += responseStringError4 + "\n";
                                                    }

                                                    List<string> DataFilePathList = new List<string>();
                                                    var filename = sourceFile;
                                                    DataFilePathList.Add(System.IO.Path.GetFullPath(filename));

                                                    FileStream fileStream = new FileStream(DataFilePathList[0], FileMode.Open, FileAccess.Read);
                                                    long chunkSize = 0;
                                                    chunkSize = fileaddresponse.ChunkSize;
                                                    long Size = 0;
                                                    string Hash = "";
                                                    int totalFiles = DataFilePathList.Count();
                                                    try
                                                    {
                                                        Size = new System.IO.FileInfo(filename).Length;
                                                    }
                                                    catch (FileNotFoundException)
                                                    {
                                                        MessageBox.Show("Nav atrasts fails , ko pievienot objektam");
                                                    }

                                                    Hash = sendfiles.CalculateMD5(filename);
                                                    int totalChunks = 0;
                                                    totalChunks = (int)Math.Ceiling((double)fileStream.Length / chunkSize);

                                                    int i2 = 1;
                                                    while (i2 <= totalChunks)
                                                    {
                                                        long startIndex = ((long)i2 * chunkSize) - chunkSize;
                                                        long endIndex = (startIndex + chunkSize > fileStream.Length ? fileStream.Length : startIndex + chunkSize);
                                                        long length = endIndex - startIndex;

                                                        byte[] bytes = new byte[length];
                                                        fileStream.Read(bytes, 0, bytes.Length);


                                                        string baseAddress = serverurl.serviceurl;
                                                        string errorString = sendfiles.ChunkRequest(i2, bytes, domMetadataAddResponse.DigitalObjectId, excelvertibas.username, excelvertibas.password, baseAddress, fileaddresponse.FileId);

                                                        if (errorString == "")
                                                        {
                                                            i2++;

                                                            float procenti = ((float)i2 - 1) / (float)totalChunks * 100;



                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Kļūda - " + errorString + " Tiks veikts atkārtots mēģinājums nosūtīt datus.");
                                                        }

                                                    }

                                                    SimpleResponse fileuplcheck = new SimpleResponse();

                                                    using (var client = new WebClientEx())
                                                    {

                                                        client.BaseAddress = serverurl.serviceurl;
                                                        client.UseDefaultCredentials = true;

                                                        string credentials1 = Convert.ToBase64String(Encoding.ASCII.GetBytes(excelvertibas.username + ":" + excelvertibas.password));
                                                        client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials1;
                                                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                                                        client.Headers.Add("Content-Type", "application/json");
                                                        client.Encoding = Encoding.UTF8;
                                                        client.Timeout = 20000;
                                                        byte[] responseArrayAA = client.UploadData(serverurl.serviceurl + "CompleteAddFileToNewDigitalObject", "post", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(fileaddresponse.FileId.ToString())));
                                                        fileuplcheck = JsonConvert.DeserializeObject<SimpleResponse>(Encoding.UTF8.GetString(responseArrayAA));
                                                    }
                                                    string responseStringError3 = "";
                                                    if (fileuplcheck.OK == false)
                                                    {
                                                        excelvertibas.errorlist.Add("Failu priekš objekta " + domObjectVariables.ExternalId + " neizdevās pievienot\n");

                                                    }
                                                    if (fileuplcheck.Errors != null)
                                                    {
                                                        foreach (var m in fileuplcheck.Errors)
                                                        {
                                                            responseStringError3 += m.ToString();
                                                            excelvertibas.errorlist.Add(m.ToString());
                                                        }

                                                    }




                                                    file_archive = "file_Archive";
                                                    file_archive = file_archive + "_" + iii;
                                                    iii++;


                                                    if (DatiXml.Rows[i].Table.Columns.Contains(file_archive))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (System.ArgumentNullException)
                                    {
                                        MessageBox.Show("Fails prieks arhivdatnes netika atrasts");
                                        continue;

                                    }
                                    catch (System.ArgumentException)
                                    {

                                    }
                                }













                                using (var client = new WebClientEx())
                                {
                                    client.BaseAddress = serverurl.serviceurl;
                                    client.UseDefaultCredentials = true;

                                    string credentials1 = Convert.ToBase64String(Encoding.ASCII.GetBytes(excelvertibas.username + ":" + excelvertibas.password));
                                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials1;
                                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                                    client.Headers.Add("Content-Type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    client.Timeout = 20000;
                                    byte[] responseArrayAA = client.UploadData(serverurl.serviceurl + "CompleteAddDigitalObject", "post", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domMetadataAddResponse.DigitalObjectId.ToString())));
                                    UploadFinishResponse = JsonConvert.DeserializeObject<CompleteAddObjectResponse>(Encoding.UTF8.GetString(responseArrayAA));
                                    
                                    string responseStringError = "";

                                    if (fileaddresponse.Errors != null)
                                    {
                                        foreach (var m in fileaddresponse.Errors)
                                        {
                                            responseStringError += m.ToString();
                                            excelvertibas.errorlist.Add(m.ToString());
                                        }

                                    }
                                    if (!string.IsNullOrWhiteSpace(responseStringError))
                                    {
                                        MessageBox.Show("Klūda - " + responseStringError);
                                    }

                                   // else if (string.IsNullOrWhiteSpace(responseStringError) && UploadFinishResponse.OK == true)
                                   // {
                                       // MessageBox.Show("Process veiksmīgi pabeigts");

                                    //}
                                    if(UploadFinishResponse.OK == true)
                                    {
                                        importetie_objekti++;
                                    }

                                }
                            }
                            
                        }
                    }
                }
                domObjectVariables.Fields.Clear();
                _tempListSubfields.Clear();
            }


            if(domObjectVariables.VerifyOnly == true)
            {
                MessageBox.Show("Var importēt "+ OK_objekti + " No " + kopejie_objekti +" objektiem" + "\nObjekti ar erroriem:" + false_objekti);
                MessageBox.Show("Objektu pārbaudes process pabeigts");

            }
            else
            {
                MessageBox.Show("Kopā importēja " + OK_objekti + " No " + kopejie_objekti + " objektiem" + "\nObjekti ar erroriem:" + false_objekti);
                MessageBox.Show("Objektu importa process pabeigts");
            }
            
            

           
            
                                
            string serviceUrl = serverurl.serviceurl ;
            
            StartAddFileToDigitalObjectRequest dom_archiveFileVariables = new StartAddFileToDigitalObjectRequest();
                        

                        
        }

       

    }
                        
}







                


            
        
    
