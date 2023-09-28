﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TriviumTest
    {
        public TriviumTest()
        {
        }

        [TestMethod]
        public void TriviumTestMethod()
        {
            CrypTool.PluginBase.ICrypComponent pluginInstance = TestHelpers.GetPluginInstance("Trivium");
            PluginTestScenario scenario = new PluginTestScenario(pluginInstance, new[] { "InputData", "InputKey", "InputIV" }, new[] { "OutputData" });
            object[] output;

            foreach (TestVector vector in testvectors)
            {
                string input = new string('0', vector.stream.Length);
                output = scenario.GetOutputs(new object[] { input.HexToStream(), vector.key.HexToByteArray(), vector.IV.HexToByteArray() });
                Assert.AreEqual(vector.stream, output[0].ToHex().Replace(" ", string.Empty), "Unexpected value in test #" + vector.n + ".");
            }

        }

        private struct TestVector
        {
            public int n;
            public string key, IV, stream;            
        }

        //vectors are taken from the ecrypt test vectors; unfortunately, svn repo is down. We took only vectors where stream begins at offset 0
        private readonly TestVector[] testvectors = new TestVector[] {            
            new TestVector () { n=1, key="80000000000000000000", IV="00000000000000000000", stream="38EB86FF730D7A9CAF8DF13A4420540DBB7B651464C87501552041C249F29A64D2FBF515610921EBE06C8F92CECF7F8098FF20CCCC6A62B97BE8EF7454FC80F9" }, // Set 1, vector#  0
            new TestVector () { n=2, key="00400000000000000000", IV="00000000000000000000", stream="61208D286BC1DC431171EDA5CAF79D9560B18ACEF26484417B651A47A3F7A80353F79AF8656DA4301A5E5A02E04265B182C67F5891220349F8CD1CD06597B77E" }, // Set 1, vector#  9
            new TestVector () { n=3, key="00002000000000000000", IV="00000000000000000000", stream="C8F9031DABF8DB03FF120D05512B5F24EAEA1BAB43201A5E93BF17F628E5B216D58577112F581A67DD5F962484ED4AC59202BA3509A73E119680B562F86DF0DC" }, // Set 1, vector# 18
            new TestVector () { n=4, key="00000010000000000000", IV="00000000000000000000", stream="F7E523040E86EA2C46A2BE705BFC62597F77E4649C0E71D51B288EDD4FC169BCD681F4603E192A7971E73290133E1E32F916D98B0D77F37927E1215C1D6AE037" }, // Set 1, vector# 27
            new TestVector () { n=5, key="00000000080000000000", IV="00000000000000000000", stream="4B430BDE0F574C7DE06E6A1918BFB4DEC0E2836071EB446C593EE1F2594533272E720E2A27992730E67D509EDF7BB0E62AEA85ED87B998FA6F53A0B77D26BBA2" }, // Set 1, vector# 36
            new TestVector () { n=6, key="00000000000400000000", IV="00000000000000000000", stream="4EAC0C5C7AD327084CFED7EAF72F6EB7FF20E11C65DFC1470C1D2EFAFCE2B1FA3ED6AFBE7CBEB677DB1189CB6892E81093B16FDC34199D0A26B89F06C86AC9D7" }, // Set 1, vector# 45
            new TestVector () { n=7, key="00000000000002000000", IV="00000000000000000000", stream="2D97F227F2463F271F853BA10806888F8BA5733564557D5174A16172BD89D7E32ADF9C5B257DCC1693AB7CA6248F7A33D311FAD4D89D1C6EB7FCF8896D94E79C" }, // Set 1, vector# 54
            new TestVector () { n=8, key="00000000000000010000", IV="00000000000000000000", stream="9EB8F6BD37474B5C2AC01BA1B3EEA5E1FBD4D4D1AE63EAFD81A4D2C900B5931273B37820BC68FCD69ED18F1ED4EC9334F39FEB330BAAEDEF2A1E51B218B385C8" }, // Set 1, vector# 63
            new TestVector () { n=9, key="00000000000000000080", IV="00000000000000000000", stream="5D492E77F8FE62D769C6A142056BE9361FA0ADD8A54601DE615EBC04C4F8B2C12A8ED2DC9AB286A0F6C49C7AB319BA6AAFAAF0CD42D0A44C7DACBC90791855D8" }, // Set 1, vector# 72
            new TestVector () { n=10, key="00000000000000000000", IV="00000000000000000000", stream="FBE0BF265859051B517A2E4E239FC97F563203161907CF2DE7A8790FA1B2E9CDF75292030268B7382B4C1A759AA2599A285549986E74805903801A4CB5A5D4F2" }, // Set 2, vector#  0
            new TestVector () { n=11, key="09090909090909090909", IV="00000000000000000000", stream="AB97616E7BAF0921F424B2573BFA15BDCA01898ABBE6AB77279AB1D732ABD10512769CC69FAB34E03B807F92C96627C17656BCC9BD9D377240A2B6FDBD784453" }, // Set 2, vector#  9
            new TestVector () { n=12, key="12121212121212121212", IV="00000000000000000000", stream="AD35E17B1971AF6B5B3E365FA64EB4CF7EBF023D520889AD71F1A07AA2E0FF44CEE32D09CF77360C52434D462B53A405EF5A60D82A0F5E3CF3321B3727A3D61E" }, // Set 2, vector# 18
            new TestVector () { n=13, key="1B1B1B1B1B1B1B1B1B1B", IV="00000000000000000000", stream="575AC77DCD54E7F48D837ADE0A88C70D777839B310C297835F5BB88C1A5FFD1569A4C4676E7D7CEA864FA1AD2F78E3E044E7D145827DD6E4095CB308111F5C72" }, // Set 2, vector# 27
            new TestVector () { n=14, key="24242424242424242424", IV="00000000000000000000", stream="5502DF8070387D1237EF5213B5F19EB79B9CC30810DB966DD2B25C9249C0378B17423F4E788BC3E82FBFC7FA3FED4A5704BA35CF9CBD12DCFD56CB6DD431A4D5" }, // Set 2, vector# 36
            new TestVector () { n=15, key="2D2D2D2D2D2D2D2D2D2D", IV="00000000000000000000", stream="FC87849624217531385850ABE1CA16D5792A45F8FC40638B0BDFB7A32D5B53CC6751FE1CBE1BA673D2113AB900CFC9C3095CA85EAAF17DC617249FEB362A3F43" }, // Set 2, vector# 45
            new TestVector () { n=16, key="36363636363636363636", IV="00000000000000000000", stream="1F9A14B98CB448D4002A3770361447F8D0FCA7B2BCFA40D618AB1833FF19DF6FB9BE5D7A47B692FE64C7CA520D60DA1C35AC0B9E608C5E459037F8E9E68B8CD8" }, // Set 2, vector# 54
            new TestVector () { n=17, key="3F3F3F3F3F3F3F3F3F3F", IV="00000000000000000000", stream="F2D9792B78C2D3EC67D8C80A710D19EADB163199C6D9A1CB8F30624D4B30D916E4C201E013EF6F4E0BA58296BAB2C561E737EDABD055F517CD546B2B22F27C66" }, // Set 2, vector# 63
            new TestVector () { n=18, key="48484848484848484848", IV="00000000000000000000", stream="435E4729820B9EF9670DACC07444211500C1F4A18134B8FA1B08067050698D2FA18B3827F5BE7DD06B75AF49CA4427880B47FA46E74D48F424ED5C981BE1B9BF" }, // Set 2, vector# 72
            new TestVector () { n=19, key="51515151515151515151", IV="00000000000000000000", stream="0FC805C2829B79F15F34AC308018C3E734B14005BE0473B19378CE115146A0940DAE4DA349905637C3981F68E430047AA3056EEF32174120D9DA49B2CF8C0EA7" }, // Set 2, vector# 81
            new TestVector () { n=20, key="5A5A5A5A5A5A5A5A5A5A", IV="00000000000000000000", stream="2C7F53F2FD7CC9342EBDB26E8245BB9F298D54B74A7E7C608E4EE6FD7A6608B69EE71B83D9635C457DD9D05FE9090FBA05E249D44218E97D1B905E4F0810A912" }, // Set 2, vector# 90
            new TestVector () { n=22, key="63636363636363636363", IV="00000000000000000000", stream="F6652E27AC1B2705B90EAAC51D79FE00093FD0FA2DC60E743768B47477E0129A2FD23E0FF656F56A39BA260FA2D45BB35BA106507CF6A0027F86C92E0174DB1A" }, // Set 2, vector# 99
            new TestVector () { n=21, key="6C6C6C6C6C6C6C6C6C6C", IV="00000000000000000000", stream="7E1A2E71199A17B0BB9A8B9B8102A9ADDA23AB4642AA1922B334D0A6B5D62F37100055CCCC7D4D5694DE2745221B6BDF02ED1F56B81BDDA070C0E09643FB31DE" }, // Set 2, vector#108
            new TestVector () { n=22, key="75757575757575757575", IV="00000000000000000000", stream="EAD8FBE63C182904DD66EDB3537915038DDEE8EEAAE44F9DB87DEF80D9DCFAA71017333258A8CADA82D35292A48CB49B4838D84E0C95A12BD5B9805FE726A804" }, // Set 2, vector#117
            new TestVector () { n=23, key="7E7E7E7E7E7E7E7E7E7E", IV="00000000000000000000", stream="7CC89FF6F0DF4C88343622535F8766884FB2A27760EC4EA0ACA75CC5907DB254EE37DF12E5C89CB0A78A569279BBCDB121C34EFBBAC3311006A6DD01DC2836C3" }, // Set 2, vector#126
            new TestVector () { n=24, key="87878787878787878787", IV="00000000000000000000", stream="01DA3DC2C02DF6FF26DBAEFA1A6C2780A66ECF539527B0FDC73D4468798F7D80E15E0633C9B71A8A5A33EBA9E29F9035E6BF125417BE003D4DD5048C8DC64BD8" }, // Set 2, vector#135
            new TestVector () { n=25, key="90909090909090909090", IV="00000000000000000000", stream="6AB0802657876C8F7211B14CE05E78024AA6D1E5F974815D398D7BB9E16855C32FB8896F57C43828D1AE034BAE3F42D61D006022C71103EDD1125CC5EBFDBCE0" }, // Set 2, vector#144
            new TestVector () { n=26, key="99999999999999999999", IV="00000000000000000000", stream="D6D25A2BE87898CE1FECAFCE8770B1214C7193240939F29545F76278FCB196D5B7D9CD12FC253C495BE60E0E3707438E2CB54214899BFAC8F5895AC439D29F25" }, // Set 2, vector#153
            new TestVector () { n=27, key="A2A2A2A2A2A2A2A2A2A2", IV="00000000000000000000", stream="EA8BD0720E569161E9BD399691518E7A54830A6B8EA55FA00C651B00B1F1EA2EC68741910B8F0367B7987024D807DB8EC7015462E6B53578EF129EE67417BF01" }, // Set 2, vector#162
            new TestVector () { n=28, key="ABABABABABABABABABAB", IV="00000000000000000000", stream="E2ED9461B719158E3F48D2D8CDACA1A13626A0BF92ECC9AB9272DA04C284A649A228B32C4607C6D0FDA518ECD5D9548C8044DCE2236F0B9415BEF8BB0E414EB6" }, // Set 2, vector#171
            new TestVector () { n=29, key="B4B4B4B4B4B4B4B4B4B4", IV="00000000000000000000", stream="736640C555214B9D30C268B8B651F614C8724314624A4FDBA75D6DC8C15DF93BEB80E8325218EA489128BBFDA19A64139510DE9B6AC006DF3B29CF7A673016BF" }, // Set 2, vector#180
            new TestVector () { n=30, key="BDBDBDBDBDBDBDBDBDBD", IV="00000000000000000000", stream="7FE26D4ED762AC9D9C1D45DC116A393C1C98F3F7181A8109475C97AE3D5FAA381F8286A55A7A9A7EAD349EB0354E891490F7EE0C5496E3FCF217D1E71A28638A" }, // Set 2, vector#189
            new TestVector () { n=31, key="C6C6C6C6C6C6C6C6C6C6", IV="00000000000000000000", stream="CACCB9A2E28AA22B80634BBA8572F27E8BBF72F76F54FDF93F6692824AEB1D35965AD35ED01571125E6EA8DEECC21290F530748CB2B43DF34081A93C8BA9F652" }, // Set 2, vector#198
            new TestVector () { n=32, key="CFCFCFCFCFCFCFCFCFCF", IV="00000000000000000000", stream="91146F1605C9F8B0B1BAA71E77C660BB77846361F43250C1F7AA9B5613E3928F4D3AFEDF6A489CC471D337549134FCCBB560A99F275BCF8604E039A338566E32" }, // Set 2, vector#207
            new TestVector () { n=33, key="D8D8D8D8D8D8D8D8D8D8", IV="00000000000000000000", stream="9B452E88539B47CFE4ACC7713C3CCB76D15BBBF36A8280917D701A9A516204FF0D0A21A38E6221E7E64F416A381FE6E048FADF96ED9E5599F5C01D97D3E2C8FD" }, // Set 2, vector#216
            new TestVector () { n=34, key="E1E1E1E1E1E1E1E1E1E1", IV="00000000000000000000", stream="1D34750D13D4C28BE03D95688A98FB000AE065F9289E5F48E070113A1FD2A7E57FDBE7410C40F4D3924032496F069BD0FA9ED7241632AB07F312D8FAEAC31C59" }, // Set 2, vector#225
            new TestVector () { n=35, key="EAEAEAEAEAEAEAEAEAEA", IV="00000000000000000000", stream="14B1062591D38C06D50B0BDF05BA594B8ADF2598145594FC06AEBBC3C1803A9F72B6D3A9BA549B35E01DC52A8B14EC66DC70DCA3BDD9728DFA030EB1BC8C3EB8" }, // Set 2, vector#234
            new TestVector () { n=36, key="F3F3F3F3F3F3F3F3F3F3", IV="00000000000000000000", stream="6F91E50A1812C42C60711F10644DF00BC2C6D0EE78B1C0D81E7FFD6AB4E2C61FAC4A3ECF2C6765693CF412E7043551186982F0051DD1820BF9A8E58511B4F1B1" }, // Set 2, vector#243
            new TestVector () { n=37, key="FCFCFCFCFCFCFCFCFCFC", IV="00000000000000000000", stream="0F5E42876722C200404CCFB421C0FC7AB78CA298FFA9243FD76181FEE0C806ECDB28BB296EEAACA1AAAD27BC5BC52A257779E10A1FB2F6D1D62DC8D91BA67FEA" }, // Set 2, vector#252
            new TestVector () { n=38, key="00010203040506070809", IV="00000000000000000000", stream="D2A8740BBA6FD9067077F9AFC0C27D4032B6AEAE50C42ECEFF255C584C0143E78CFA4E3EBE03074F23D762D0A7563521BE755B2166CD920EECBB5DB84737FA01" }, // Set 3, vector#  0
            new TestVector () { n=39, key="090A0B0C0D0E0F101112", IV="00000000000000000000", stream="1849D8F8D00D4FD49CA825C40654B49ACD75DD143661F099A12911E14AFB9A35E6A19303809DE3B5956178A9CC6E29A8CC97F1A3C47AA3360B4819ADDF4FFF7D" }, // Set 3, vector#  9
            new TestVector () { n=40, key="12131415161718191A1B", IV="00000000000000000000", stream="D24D36A07F26A03280CE4C2A671FC7B617E92D231F649128EB4D17070003C8D365DA16662EFFA87679E6C4E557F84E12E69FB65A496394D35DECBC9714C16BB1" }, // Set 3, vector# 18
            new TestVector () { n=41, key="1B1C1D1E1F2021222324", IV="00000000000000000000", stream="CC84F164802715C020425A7FF64C1A0B033D52B940AC2CC91F0737592951826A76D190B92661E4FAD4750451D6E365CEFF041FAB358A09B5DDDAD4AAD6FA460E" }, // Set 3, vector# 27
            new TestVector () { n=42, key="2425262728292A2B2C2D", IV="00000000000000000000", stream="2DE8D0F2BC8176D633CA1052E1554C4D54A2118E4776445251193CE6C1A439594DAE1126EF6A78E62AA19B843BF60DEC261D6DA1EA64C1449D83E83D29267747" }, // Set 3, vector# 36
            new TestVector () { n=43, key="2D2E2F30313233343536", IV="00000000000000000000", stream="0B4A173B35C6705A4DB98C4D0FC70B8F4C1EB77A823B550C296770F2C0CEED87B47B66E828CD39723663D60578C676AB39AD5F3BE5D2676E685C1143BFF19A52" }, // Set 3, vector# 45
            new TestVector () { n=44, key="363738393A3B3C3D3E3F", IV="00000000000000000000", stream="BE006EB16B3554B2F3B44E22F6FA657A3567A9B62F96133F300ED2843AE6E5247BABD8521C2099DF4080A51B022BC53D881BB63766150D26EA5258A1B4412FBC" }, // Set 3, vector# 54
            new TestVector () { n=45, key="3F404142434445464748", IV="00000000000000000000", stream="6A3E215FD1A2A55AEC4C84BB216494FD81C17279D131527A9FD4F3B04350B0C7365B375827950AD45EE8388E00F8A8FCF021B3D63FE031A7CF4E8D0EB46B0155" }, // Set 3, vector# 63
            new TestVector () { n=46, key="48494A4B4C4D4E4F5051", IV="00000000000000000000", stream="863AFB906F2A73DFCADFBC47EEEE2031CEE0AF8847777825AFD141D8897A9E96C5F9284A391CCEC34A199EE8228B0B981ADCC859B9008754D3D0B99F136EF4E9" }, // Set 3, vector# 72
            new TestVector () { n=47, key="5152535455565758595A", IV="00000000000000000000", stream="252C4021B2FE2C6176376D4D905DCC59C502884E87E2E27F54AC46F37B62234B46C7863F75438884DB124A3AAC258D50611275FFC8F20FB94E8FC45306A7A86E" }, // Set 3, vector# 81
            new TestVector () { n=48, key="5A5B5C5D5E5F60616263", IV="00000000000000000000", stream="34E30006E62AF394ADF5042EAF6F859839F13ED4519AB0CEE2590F93D802011E1C1F257839054060A55B28BF157ED8B5DD8E54E92415AB12844CAF90498C2023" }, // Set 3, vector# 90
            new TestVector () { n=49, key="636465666768696A6B6C", IV="00000000000000000000", stream="41DF606120C6447801EF9BCE34C659FB1855A59BF575379C34CA364D029C722BDA107F619295D212B7E23F9CA0788F85EBA749FD9DAC94902943AC65C768B579" }, // Set 3, vector# 99
            new TestVector () { n=50, key="6C6D6E6F707172737475", IV="00000000000000000000", stream="4D0E814EBBC40E61B330C8A4678DEBF9FCED0C71298E1159CB903DEE6EBDC294D9AC00441438441D61F61FE421C4F8A839C715B0A9082FE8921DB51E14B188DA" }, // Set 3, vector#108
            new TestVector () { n=51, key="75767778797A7B7C7D7E", IV="00000000000000000000", stream="8958E8ACAC755678CE2D4F5C482AD71AC7AFE4CF8E5062555906EF8DEBBA30A6EBF09D9FDFD2ACBDC4619199BA95C6B6B145D084E61B4EDF02693DFC3DE76A54" }, // Set 3, vector#117
            new TestVector () { n=52, key="7E7F8081828384858687", IV="00000000000000000000", stream="CAE3F9794BE605739E8B133E4DD97AE06D00780A66F1BDC3B856D178D263EC282EC6775E89B15A429307643BAD6042CFFC248E8D082475FB5FFFF84EC5E2F5C7" }, // Set 3, vector#126
            new TestVector () { n=53, key="8788898A8B8C8D8E8F90", IV="00000000000000000000", stream="C204861394C3B27301D2B216AF74F01B36CD63EACC07A6FB8FB60971F3C4160C72E009D795B74B9F826B264DFC1FFB9D334CC9CA88C03009B6AC5ED59F70A259" }, // Set 3, vector#135
            new TestVector () { n=54, key="90919293949596979899", IV="00000000000000000000", stream="0972B6F8B7CEC540EAF230E2465AC6A305D78A9E5CCBB38A013F6674A36DFF227E292760C637629C3BEC82E56AB550903E3D559D1648462B8E2535E2B2132E3A" }, // Set 3, vector#144
            new TestVector () { n=55, key="999A9B9C9D9E9FA0A1A2", IV="00000000000000000000", stream="1C35DA8526E84D18F9F319DBC6488548FFB3A460DE4C70E8CD95EF7C0A342620D76119F17C4E56B17239FC1785469A070AE68D917931F73E190936D0B918EE5B" }, // Set 3, vector#153
            new TestVector () { n=56, key="A2A3A4A5A6A7A8A9AAAB", IV="00000000000000000000", stream="412AB1D18E675AE9626553235CB4EB9390694F9E1B793EA64EE1EBB9CA315292EE9AFE32827FA7993F355F026C874EEB6B89EF05D0EBE2BC0D8CC7CCDF04A177" }, // Set 3, vector#162
            new TestVector () { n=57, key="ABACADAEAFB0B1B2B3B4", IV="00000000000000000000", stream="01E759B71C1EB69C81580610006BBA1AE6962D7ED9FB6F804611D20DFBBE47F62527B3F8102876C5467CB4844723C979CA4A91FDCFEC8637578F7E46B8301F10" }, // Set 3, vector#171
            new TestVector () { n=58, key="B4B5B6B7B8B9BABBBCBD", IV="00000000000000000000", stream="A1C1EE5C63AB5C0655EB6957C4B6C846585A432D6EE6F42661B4EBDEA363CA1ECB9950BD1C59B8D75C1362E0768958CE0047715335DE0ACC4228B8EBE4017036" }, // Set 3, vector#180
            new TestVector () { n=59, key="BDBEBFC0C1C2C3C4C5C6", IV="00000000000000000000", stream="B7BE8BF5D02E37871E6257092232E6B47DB4026F9F5227F1096731FC3CD66AFA9B3D04520BA87785C43F401F40302758194BE49CA8CF00A58D991F40B9662F56" }, // Set 3, vector#189
            new TestVector () { n=60, key="C6C7C8C9CACBCCCDCECF", IV="00000000000000000000", stream="10FBD1336E81B0D149A9FD88CCA7DAA917CCFF08B8B1D3675C27FD6270130134C24847ECF7EE5C1DC02E6E6634FF1EE2F0889F607F2AB072DC81799B8B861D4E" }, // Set 3, vector#198
            new TestVector () { n=61, key="CFD0D1D2D3D4D5D6D7D8", IV="00000000000000000000", stream="90B147BB4CE3844664E75678B78F02C2925687A91C4481918E8784EA4C51A698D52B2D16C6C2A8B58C61A7786E4A3BE715689BFE8E251A46EBEEE2E422056F9C" }, // Set 3, vector#207
            new TestVector () { n=62, key="D8D9DADBDCDDDEDFE0E1", IV="00000000000000000000", stream="1ADB5D07C885936394FDD08666830F5E3F30CDD1B94D9B1804108B79E594D3911E0D195B8099E6766277DE88A7C2B8106094B64359DDAD8B77E199167CFA1343" }, // Set 3, vector#216
            new TestVector () { n=63, key="E1E2E3E4E5E6E7E8E9EA", IV="00000000000000000000", stream="B620614F87449DFE64BE90AE86F61216E342838490C10838ACFE1BDDF6481763936DA9E1FEB6DCB635605C94CCB0E167962B84037CC274EE964DEEE41CAF0F85" }, // Set 3, vector#225
            new TestVector () { n=64, key="EAEBECEDEEEFF0F1F2F3", IV="00000000000000000000", stream="8E9E13AE97E14098B9DF8672AE245286CE437159905F026515498ECA6F9122B5EED90AF2330DF448D18EE4D638D8BEA3CBE93497319CFC8E5DD387B6129121C9" }, // Set 3, vector#234
            new TestVector () { n=65, key="F3F4F5F6F7F8F9FAFBFC", IV="00000000000000000000", stream="AEF73417AE8120FE8B785E0C6671C689C58892D394E8AB8EF6B926558DCA31D0F974E26B13EAA417D932AA9A7BE270FAF8ACCB4C0CBD396040DDE6C912490643" }, // Set 3, vector#243
            new TestVector () { n=66, key="FCFDFEFF000102030405", IV="00000000000000000000", stream="496191DC1EDC22DE848E61CA625E3A7AAA407E459DA413BD1D19401DC13160ABF74F384A9DC921157213373E268479B5709A132C11C4798C5B1D285D95308EAC" }, // Set 3, vector#252
            new TestVector () { n=67, key="0053A6F94C9FF24598EB", IV="00000000000000000000", stream="A1809640CE79C540802F49D32023B2EA8A398428F4A5EFDB0A124FDDF39D23CEFE5257EE1BCADC0954049102826E144CA96E65D4A169823FFF20E832880B7B8E" }, // Set 4, vector#  0
            new TestVector () { n=68, key="0558ABFE51A4F74A9DF0", IV="00000000000000000000", stream="203612E1D1F22BD47314E9C03DDA5BB7579F5C499983A481F8FE21C9490093D5EC7C63FDAC95AD9537DC9E87CD143988D97BBCCDB9E348519D07DB0C43E4A069" }, // Set 4, vector#  1
            new TestVector () { n=69, key="0A5DB00356A9FC4FA2F5", IV="00000000000000000000", stream="22E1C692C76EE198113F84A3D73B6C5A2EC7CB8B775AA68759471EFC86B3FA3203BBED11817CDFCC82FFDE2EA2FDE7D14666369636C6958C896BBAE5EB0C51C1" }, // Set 4, vector#  2
            new TestVector () { n=70, key="0F62B5085BAE0154A7FA", IV="00000000000000000000", stream="124B35E1F161A9FFE3D65A9041D5235FA3659DC9BE659A47C16A0C2E6A7519CE8892744E88FDD4032395C02A07B7BBD6CF780FCFAB6B179E608EC9B07B9AE61F" }, // Set 4, vector#  3
            new TestVector () { n=71, key="00000000000000000000", IV="80000000000000000000", stream="F8901736640549E3BA7D42EA2D07B9F49233C18D773008BD755585B1A8CBAB86C1E9A9B91F1AD33483FD6EE3696D659C9374260456A36AAE11F033A519CBD5D7" }, // Set 5, vector#  0
            new TestVector () { n=72, key="00000000000000000000", IV="00400000000000000000", stream="ACBB386876653D15010DEFA7C65B36D701CFAF927B417550BE32D0444A24DEB589159B965C6740823F6BDFC378174AE2F664DCA0B68C621D2775BD13E6A788DF" }, // Set 5, vector#  9
            new TestVector () { n=73, key="00000000000000000000", IV="00002000000000000000", stream="88BD48945DEA0BEB94D1F13FC589D61F4961046D4054B2EC274709DB1D8CC5472D1CD07D3CEBFC31E56DFED58029E598FB45D1954B6C86C9CC5EF422FFADFE32" }, // Set 5, vector# 18
            new TestVector () { n=74, key="00000000000000000000", IV="00000010000000000000", stream="890782471E32E042C14767285A9BBD89605FEE5E38B9E78E3D750821AC7B4864A28DA27EB2EBBD6413CC6A5066E5240506E3F37C22876A7E9557C6B1BE1CE300" }, // Set 5, vector# 27
            new TestVector () { n=75, key="00000000000000000000", IV="00000000080000000000", stream="33394D41641D0D8E5778CB722C1436300923F40194B951216FAA8BBE0A6EC7FC23724F8DD4E4E922F9D9B99579C5765DFC5A8C268C1CAF0C114CF814DB2239D8" }, // Set 5, vector# 36
            new TestVector () { n=76, key="00000000000000000000", IV="00000000000400000000", stream="27882DB265B89E59AD89611880FE6D125A4E98744C91CBFE1706E65A0A00A1EE3C020004AA287ECC209EA77650459840D73BB289482C732A024941E72E3244F0" }, // Set 5, vector# 45
            new TestVector () { n=77, key="00000000000000000000", IV="00000000000002000000", stream="FDF27A99D3144024D2363B9882D36D82C505DA7B7134C0D93EA0C35BC909D90EFB59B3E631F717297C7EBD7FE4F20FC24E838C3651B5B6AC38648C4B79228B3A" }, // Set 5, vector# 54
            new TestVector () { n=78, key="00000000000000000000", IV="00000000000000010000", stream="AE7F060AC8B0051C1ACBCBB2E7F10E6FAD4C980FB86F62FEAFCD7345D624485EC4D7773C1FA45FFA5234D2114BD4C48BC257D77BC524519BF00E1E9347B19858" }, // Set 5, vector# 63
            new TestVector () { n=79, key="00000000000000000000", IV="00000000000000000080", stream="82E7FCB2193DD7CCBDFB30E4A1BC740E2756EF94676A6D4B01B1DD2F2A335BB6BCB85B1086D6A36BBA4F98488F2A74B8F85EFBFC8082347BB8C71B1BD8682D1D" }, // Set 5, vector# 72
            new TestVector () { n=80, key="0053A6F94C9FF24598EB", IV="0D74DB42A91077DE45AC", stream="F4CD954A717F26A7D6930830C4E7CF0819F80E03F25F342C64ADC66ABA7F8A8E6EAA49F23632AE3CD41A7BD290A0132F81C6D4043B6E397D7388F3A03B5FE358" }, // Set 6, vector#  0
            new TestVector () { n=81, key="0558ABFE51A4F74A9DF0", IV="167DE44BB21980E74EB5", stream="A850A970ABCF5F73BCC5DB76F6B5E856362F1B36AC498D05C20FBE7763598DE1FD98B03CC54060E8C9C19B16490C665C3636A03BAB46656A695ED75F0E659F04" }, // Set 6, vector#  1
            new TestVector () { n=82, key="0A5DB00356A9FC4FA2F5", IV="1F86ED54BB2289F057BE", stream="DE9410C5134BCBADD0D2D95684E838183B91B0E8C1FA173C38F5B75103ABF0B8546EDDE22D6BFB3BF1C0754C6C42982FAEA5A3BD03DAFC0D1586E389B78C5872" }, // Set 6, vector#  2
            new TestVector () { n=83, key="0F62B5085BAE0154A7FA", IV="288FF65DC42B92F960C7", stream="A4386C6D7624983FEA8DBE7314E5FE1F9D102004C2CEC99AC3BFBF003A66433F3089A98FAD8512C49D7AABC0639F90C5FFED06F9D35AA8C86630E76A838E26D7" }, // Set 6, vector#  3
          };
    }
}

