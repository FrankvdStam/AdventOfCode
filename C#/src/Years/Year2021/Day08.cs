using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day08 : IDay
    {
        public int Day => 8;
        public int Year => 2021;

        public void ProblemOne()
        {
            var count = 0;
            foreach (var l in Input.SplitNewLine())
            {
                var split = l.Split('|', StringSplitOptions.RemoveEmptyEntries);
                var words = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                count += words.Count(i => i.Length == _segmentCount[1] || i.Length == _segmentCount[4] || i.Length == _segmentCount[7] || i.Length == _segmentCount[8]);
            }
            Console.WriteLine(count);
        }



        public void ProblemTwo()
        {
            var score = 0;
            foreach (var l in Input.SplitNewLine())
            {
                var split = l.Split('|', StringSplitOptions.RemoveEmptyEntries);
                var patterns  = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var output    = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                score += FindScore(patterns, output);
            }
            Console.WriteLine(score);
        }



        private int FindScore(string[] patterns, string[] output)
        {
            var one   = patterns.First(i => i.Length == _segmentCount[1]);
            var four  = patterns.First(i => i.Length == _segmentCount[4]);
            var seven = patterns.First(i => i.Length == _segmentCount[7]);
            var eight = patterns.First(i => i.Length == _segmentCount[8]);

            //Seven has an element not contained in one, of which we know the position
            var a = GetUnCommonChars(one, seven).First();


            //segment f is off only for 2 - we can find 2 and segment f with this. We will also know segment C (because one only has f and c)
            var two = "";
            var f = '\0';
            foreach (char ch in "abcdefg")
            {
                bool found = false;
                for (int i = 0; i < patterns.Length; i++)
                {
                    if (patterns.Count(j => j.Contains(ch)) == 9)
                    {
                        two = patterns.First(j => !j.Contains(ch));
                        f = ch;
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            var tempc = one.ToList();
            tempc.Remove(f);
            var c = tempc.First();


            //Now that we have 2, and we already know segment F, only segment b is off for 2.
            var all = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            all.Remove(f);
            foreach (char ch in two)
            {
                all.Remove(ch);
            }
            var b = all.First();

            //digits with count 5: 2, 3, 5
            //We already know 2. We know b. 3 has b off and 5 has b on.
            var five = "";
            var three = "";
            
            var temp = patterns.Where(i => i.Length == _segmentCount[3] || i.Length == _segmentCount[5]).ToList();
            temp.Remove(two);
            if (temp.First().Contains(b))
            {
                five = temp.First();
                three = temp.Last();
            }
            else
            {
                three = temp.First();
                five = temp.Last();
            }

            //0, 6 and 9 left
            //six has segment c off, zero and nine do not
            var six = patterns.First(i => (i.Length == _segmentCount[0] || i.Length == _segmentCount[6] || i.Length == _segmentCount[9]) && !i.Contains(c));

            //use five to figure out e
            var tempe = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            tempe.Remove(c);
            foreach (var ch in five)
            {
                tempe.Remove(ch);
            }
            var e = tempe.First();

            //segment e is on for 0, off for 9
            var tempz = patterns.Where(i => (i.Length == _segmentCount[0] || i.Length == _segmentCount[9])).ToList();
            tempz.Remove(six);//six is the same length as 0 and 9
            var zero = tempz.First(i => i.Contains(e));
            var nine = tempz.First(i => !i.Contains(e));



            var values = new Dictionary<string, int>()
            {
                { zero, 0 },
                { one, 1 },
                { two, 2 },
                { three, 3 },
                { four, 4 },
                { five, 5 },
                { six, 6 },
                { seven, 7 },
                { eight, 8 },
                { nine, 9 },
            };

            //Output strings have different ordering then the pattern strings.
            var remapped = new List<string>();
            foreach (var s in output)
            {
                foreach (var k in values.Keys)
                {
                    if (UnorderedEqual(s, k))
                    {
                        remapped.Add(k);
                        break;
                    }
                }
            }



            var result = 0;
            result += values[remapped[0]] * 1000;
            result += values[remapped[1]] * 100;
            result += values[remapped[2]] * 10;
            result += values[remapped[3]] * 1;

            return result;
        }

        private bool UnorderedEqual(string first, string second)
        {
            if (first.Length == second.Length)
            {
                foreach (var c in first)
                {
                    if (!second.Contains(c))
                    {
                        return false;
                    }
                }

                foreach (var c in second)
                {
                    if (!first.Contains(c))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        
        private List<char> GetUnCommonChars(string first, string second)
        {
            var result = new List<char>();
            foreach (char c in first)
            {
                if (!second.Contains(c))
                {
                    result.Add(c);
                }
            }

            foreach (char c in second)
            {
                if (!first.Contains(c))
                {
                    if (!result.Contains(c))
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }




        private Dictionary<int, int> _segmentCount = new Dictionary<int, int>()
        {
            { 0, 6 },
            { 1, 2 },
            { 2, 5 },
            { 3, 5 },
            { 4, 4 },
            { 5, 5 },
            { 6, 6 },
            { 7, 3 },
            { 8, 7 },
            { 9, 6 },
        };



        private const string Example = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

        private const string Input = @"eadbcf faceb faecgd gdefabc adc ad adbf gfacbe bceda dcegb | gdfcae adc cedbfa dafb
ed acegbfd defb ead dbcfae dbeca caefdg bgecfa dabgc efacb | gfecab ed cdaegf de
fda dfbeg cegdab fa edfagcb acgde dagfe abcgdf ceaf dfecag | fgcead af caedgbf bdfeagc
fdae cebgfa df bdf ebcgdf bcdeafg cfbdae afebc fbadc cbgda | df cbgfae eadf bfd
facgb gdefabc decbfa cb cba cgfaeb gafec bgfad agcefd bcge | cfdage bc dbagfce gcbfae
eacfd caebdfg egfbac gaced abfde cf gafcde adbgce gcfd fec | fec ecf cf dgfc
ecfgd fc fec cdfa defcag degbf dbcgaef eacgdb bfagce adgce | egcda cfdge cgefda dcfge
afbed eabgfc cbedag ac egdbfc gacf abcfe becgf aedfbgc cea | ca ac feabd fcag
ecfgbd agebc dfbae fcedgab adgc gfbcae abcged dc cebad cbd | acdg begca cd dc
cadeg gb dgaefbc gbd acbfde gabdfc gbcad afdbc abfg degbfc | fegcdba bg dbfceg gdb
ac dbagf dbcgfea acfgb gcad caf eacbdf dagbfc gcfbe efgabd | ac dgac egcfb deafbcg
cafdbe gefdc fedba fagb gbfeda bgcfead ecgabd ga gda efdag | defga dag aegdf abfg
faceg ab gceab dgbefc bcegda bgfdace gecdb gbad bac bafdec | acgdbfe cebag edfgacb dabg
dcegaf faedbc acbdg dfcag afd gbafce efbgcda feacg df degf | fbagec acgbd daf gdfe
agd dagbf fegbad bfdea bfgdc gedbcfa fdcaeg cfbeda ag bgea | bfaed adg dabgef gda
fabgde gf cafbe bfg afcdebg cdfbge cfgd cebgf baecdg cgbde | fg fg gf fbg
fgabdce gcfeda agcdbf gecaf ebagf ecbg bag fabed acefgb bg | gacfdb bag dcgeafb aecgf
gfdbea bedag gefdc bdca becagd fdbegac febagc bgc gbdce bc | gbdec cb caegdfb ebadg
agefdbc bdea ba fcbeg eacgb abgcfd gceafd cbeadg gab acedg | gfacde cfgdeba cefadg dfbgac
cbegfd bafd gbfcae ba acfgbed egcda dbcfg acb bgafcd agcdb | cab gfcdab degabfc abc
gcbf cgadb cdeag ecadfgb fdebag afdbg bc cab cfgdba cefbda | cb bfdgeac cba cba
dcfge cbadge acedf fge dgfceb dbegc geadfbc gf edagbf bgcf | cefadgb fge gfe efg
ebdagcf gadfce bd badef adgbce eabfg abd cebadf edacf bfcd | aedbf dfbc adbfgce bd
eacfdb cef gcbeafd ce defgc bfadgc bgfdc fedag ebgc egcbfd | cfe bgcedf ec gfcde
dbefa fbge gedfba dbgcfa gb dgb dbgceaf dbgea cdfbea egdca | bdg afdbcg gb fbdagc
gafc dgeacb eaf af abefc ebgadf cefdb efagdcb fgeacb becga | af egdbacf af aefbc
dgbca dgaef beac dcage gbafcd cgebdfa fdbgce ce cge dcbage | bacgd caeb gbedcfa gec
ab efdgba cdab bfceg gfadc abg cgedaf dgafebc cgdbaf cfgab | fcbga gfbca ba edcfga
ecdbf dcgbafe gcbdef fcdaeg gdf bfega gdbc fdbge cedabf gd | dgcebaf gebdf dbgc dg
dbgac cbd aedcfb gfbad gfcdba bc afebgd gcbf gcdae bfgadec | gbcedaf cb fbcg fbcg
dfagceb ed bgadfe bgafd gacfdb adef bgeac bgdae fcdgeb bde | baedg defa adef dbega
bgfeac fea aecdgf egfba abce begcfd ae gfecb bcafgde bdagf | afe bfdgec ceab ae
egbcf afcegb dbcef ebgcdf cfead bacedgf db gdfbae cgbd edb | edb decagbf gfcaedb aecbgfd
gaed egfca eac bcfeg abdgfce eafgdc cfadg ea cbdfag aebcfd | dfegabc cea eacgf efbcda
agbcde edgc gadfb gcbea bcfdae cgbaef dca cd cagdb fdagcbe | ebgdca dca ecgd edcg
gfdceb cdefa geacdb befgacd gd abecg gcd badg deacg bfgeca | bgcadef gbad defacbg fdgabce
bagfc gc cebaf bdagf gdbace ecgf gbcafde bdefac cbg eagbcf | acfbg gc fceg cgb
efc fgedb efcagd dfcab acgdbfe fbacdg ce bfecd aceb fecadb | abce defgb ce ec
gadcbef bdfe cgbeaf fea cdaebf acdfg fe ebcad ecdfa beacgd | efdb edfb ef ef
aebfc ebcfgd dcegafb bfaced gb bcg egadc agcbef cgbae bgaf | gafb bcfdeg cdbgfe gabf
fgbce befacdg fb gafce bgcfea cedgfa bgcde fbag eacdbf bfe | bef gfbeca bf ebf
efbag fd adcbef abegcd fdgecab gdbfe dcfg fbdgec gcedb dfe | efd gdcf dgfc df
cdeabf gcde gbfdae ec cfgab abgce cae bgcdae gdeab gfcdeba | eacbdf bcdfgae aegbc aec
bcafge bdae fdcge fbdaeg dgfae aef ea cgfedba gafcbd dabgf | ea aef deba aef
egfd ebcgaf fbecg gd dcg cadfgb eabcdfg fgecbd cegdb ceabd | dfagecb fdbgcae bdcea fgecb
bedcgfa dbecg abcfdg fd cdfeg fgace bdef gbfedc gdf gcadeb | dfg bgacdef cedfg dcgfeab
fcgdaeb eagb cabfe gb dgcef gcfeb gbc befgac fbacde adfbcg | gcdfab gb gdfabec beag
cgabfe badfge aegd gdfcb decbfag de dbgef befadc bagef dbe | fceagb bcfgdea becgaf gfedb
fcd gedf fdaebcg abgcfd df faceb edacg gabedc fdcae afegcd | cbaged aefgdc fagbedc dfge
cedabg be edgca dbecgf cageb cedfag gfcedab cbgfa adbe egb | bdea egfabdc be gaecd
bagfe faecgd bdgeaf eabgfc gdacb fbce cgf cgfebad fc gfabc | cebf bgdecaf fcg cgf
acdfbe abcde dcegfba fegdc bgcade fdcea acefgb fa aef badf | fgadbce fa acdfe fedac
gbcef gefacdb fca afgcbe efgcad gbac ac dbcfge cfbae eabfd | bcfgeda ebdaf agbc ac
abdeg gdeafc bagdf gedbac fbge bdeafg gf fbcda fag gbdeacf | bgfdae bedcafg fcgade fegb
bcadg cbd edgcbf afdc bacfg cd cbagdf dgecfba egfacb bdega | abgde cafd cadf dcb
gcb fbgcae dgfbc egdc bdagef abcfd gc fbdceg befgd eabcdfg | bgc cbfdgea gc edgc
dgabe dbe daegf beac fgcbeda gecadb eb bdfgce cbgadf bacgd | gdfae abec bgdae eafdg
deagfcb gc cdga gafbde baefc egcdbf fadeg afcged gce eagcf | bcfdaeg egbfdca acdfegb cge
adbfeg dafgb bgdacf gfbde fe fega bcgeafd beadcf fbe gdbec | gbefd agfe egfa fbe
defbga fagdc fdace cfg gbfda eagbfc gdfaebc gc cdagfb dbcg | cg cg cbdg badfecg
fbecdag gfdeb dgcbaf afcbed ec cfaebg acde ecb fdabc fedcb | bec cead ce adce
dfa gdbecf dfegc egafd cbaedf dfcebag ebgda dcegaf af cafg | eadfgbc daf efgad af
gfdbe fdgcb gecfda egbfacd gfaed efb eafcgb be daeb bfdaeg | be feb bgedfa gdeaf
egcbfd bacfg bdfegca bfdgc edafbg cegba fagbdc adcf fa baf | abgfdc fagcdb af fcda
dfcebg afdgbc gaf ecga ga afgbe fabegc defcgab feabd cgbef | ceag dbcegf afg bfeagdc
adgbfe fbd beacfg fecd fabdc agbdc fcbeda df acefb fagbdce | cedf abcdefg dfabc dfb
cfaeb cgdeab fgced afdb cabedf db cabegf egdbfac cdb cefbd | dcb abcgef agdecb decfg
dcgbae aecbg agecf dface bcagdf fga cbfgae bgfe fadbceg fg | gf facbeg fg fg
cafbdge bcgea af dcfage gbdcaf bafd fdbcg fga bafcg gbecdf | bacgdfe cafdbge cfagebd fgdebca
fcabge afebg edgb dbgecaf abcfd fdg adgefc bgfda badfeg gd | febga ebdg gedb dfgba
caefbd fgda fbcga gebca fbg cbfad dagfbc gf egdfbac fbgecd | feabdc dgaf gf gfdbeca
egc bfcag gedcafb ebgcf fbdcge ceagfd bdcaef ge fdbce begd | afbcged cfgdaeb fabcde bgde
bcae gdcfbae egcbdf dfceb dfega ac agcbdf adfec adfebc cad | aceb gbcfad cagdbef ca
eg afge gefcabd cadgfb cgbae gedbcf ebg gbfcae abcfg ceadb | gadecbf dabecfg bge egb
cfge ecgadf eafgcdb cg gbecad fdacg cga cfbdae acfed fbdga | eabcgd fcaed gdacfe cefg
ecgabfd fgbde cdfa da eacdgf fedga efagc dag abegdc gefacb | adfc faegcb dcafegb agd
de cbdef eadc fcbdag fadcb gcbef befgda bdafce dfe gefbcda | fagdcb deafbc gaebdf gebafd
bdga dacbeg edg ebdacf fgacde dbceg bcaed becdagf gd cgebf | deg abgd egd gdeacbf
bcefg afge egfbac ebgfacd ef bedacf gdebc efc acgbf fagdcb | cadfgb efc cfe gdcbaf
fd cfd fdcbge dgacb bacdeg fagbdc agfd befca fgdcaeb abfdc | df cgdbeaf bafcd aebdfgc
be degfcab ebd abeg fcbdag eafcd gfebad defcbg dfbag badef | eafgcdb dcgebf bfdeagc edb
dbfagc bgfcdae cagdbe bdcf geabfc fgb bfadg bf faged dbgac | afbcegd dcbf gdbca bcdf
ebgca abcegf cbgefd acg gfea cdefbga ga ecfgb cebad gfacbd | afge ag ecbfgda bdgcef
agcfb facbe ef afgedc fegb efa dgabcf cedab eabgcf deacfbg | afe ecdbgaf gfbace cbegdaf
aegcdbf ecdb gfbadc afebg gdbcf de afcgde dgbefc def efgdb | geacfbd ebcd de dbec
gbe bacfg ebagc cbagef geadfb ge fagbdc deabcfg fcge cbdea | eadgbf gbcfa bcgaedf eg
geafc fgaced dgefbca gafed gde cbfgde aegbfc cagd gd daefb | dabcegf bgefcd dg cdga
fgbcd fabc fcdbeg gcdfa fcagedb gafed cag gfbcad ca cdgabe | faecgbd acg gdfbc aedgf
gafcbe bca cedb cegfbad egabd bc abdgc afbdeg fdagc daebgc | gbdac gbacd dcbag cedb
ecg cg cfbeg gcdabe feagb cbefd fgac ebfcga gfdabec adefgb | gce gdebac bfedc gefba
febdc ebda feb dbecfa decgf gafceb fbcgad bdcagfe eb bdafc | edcfb fbe be cfbgda
gfdcb bedfac abed cbdfgae cdabf afd faecdg cfbea caegbf da | da ad fad da
abfgdec bafgce fbdge bgfad de dcegfb cfaegd bdec deg gbfec | dge bfgecd dceb ecfgb
cdfe gcadeb afgdec egabf ce gecfa gcfda ceg ebfdcag gfadcb | cge dcef cdgfbea egc
da agfebc gdbcfa dafcg bdacgfe ecgfd dbfeac cad agfbc bgad | deabcfg dca bgcfa bdag
dgbfa efd afcbdg decbgaf fabe fedagb fe gdecfa cdbge fegbd | caedgfb dfe abfe dcbge
gc acg cabfe cbeag bcfedga bagecd gdbae cgdb edfgca gbeadf | gac cgdb cg cdgfea
cdafbeg dfecgb gbadc bgcaef bfcge dbef gfbcd ceadfg dfg df | fd bdagc dfbgc gafdce
dbgaf badfc badge ebcfad gf abcfgde cdabfg afcg cbegfd fgd | fg fg cfedbg fg
fcgbe cbedafg deabfc gdacfe dega efdgc gd dacfe agdcfb fgd | dg eabdfcg dcfega cafged
eadgcf cgebfa bfgd fbe afebdg dcagfbe bf cdeab fdgea abdef | dfgb bf fbead degafc
cafgeb gadfb cabfgde df bdef dgf cadgb bfgae cfgdae dbefga | fdg gdf bgcda dfg
gdfceb acefd gdafe ca acd bfac fbdce bgceda ebgfdca bcfead | acbf cbfa fabc dac
cbfegd gfced befacdg fbgde afbgde dc cbdg decbfa gefac dec | dgcb dcefg edc cd
cg dfbca bdfeg agcd bacegfd cabfed cgdbf cgf bdgacf gcefab | dbegf bdagcf gc gcf
cafdg cfgbe efdagc fgbdc bdf bfegacd bd cfbadg bagefd cbad | db edagfc acdb dcgfea
fbagd cfga fcgeabd bfa fbged fecadb cfagbd abgcd egbcda af | gfacbde cagf abdfgc dabgcef
fd gbeacf caegdf gdef cbafgd ecgfa daf bfdaecg faecd adcbe | bgcfad efbgacd afcged deacb
fcaegd dfaec fgcbda cbaed fdcaeb eabcg cbd bagfdce db fedb | ebdf ebdf bdef fbcgad
fcdeb aebdgf cfgd gdbec cegbafd deg efgbdc bedcaf aecbg dg | gd ged gde dge
agcf fecgab fc fgecb bcf abefg aefbdc ecdbfag dbgce gdefab | bdfcega decgb eabcfd fc
bgcfd gfcad gfa adbegc afegcd gcead faegbd cefa abegcfd af | fa af gdcfb bgdcaef
efgb gadfbe bgdcae fdceab gafde dagbe gacfd fe dfe ebcdfag | fbeg fgeda dcfga dagcf
eacdgf gbaec fdbeag dg dafbegc adg gfcd faced daecg cbefad | dgaec dag cgfd afdec
ecbafg fgeab adecf abdgecf bafdcg gc cagfe gafbde cbge fcg | agbfde bgce egbc fegbad
decagf bedacf gabcfde abf deba cfgeb ba afced gabfdc beacf | ba ab eafdcg daecf
fdgbaec ac fcba bdfec agedb adgfec cad adbcef cadeb egbcfd | edcbfa dca fbca acd
agfebdc agbfcd bd fegacd abcge bda bgdf bcdeaf dgafc gdacb | dfceab dcabg adcefb fbdcea
faebd bdcge eca fdbecg dgac ca fecdagb feacbg ebgdac daecb | gcad cadeb eac aec
abgecf cabgf efgcad cgf adebfg fadbc bgce gc gfeab ecfdagb | gbaef gbfae ebcg cg
ecg dcfebg degaf cg gefac gecbdaf cafedg gadc cafeb adfgeb | cge dbeacgf dbfcgae bgcefd
adgfbe fcabegd da daecfg afd begaf cdfeb bdag afbcge ebafd | gabd dgba bgda fdabge
bfgac bfdcega dfeg abgedc cadbef df adcfg fdeacg egdac fcd | df gefd edgf df
cbe efcgbd gcdfe cafedg be cebdgfa gbeafc cadgb dbgce bdef | ecdbfag debf cbe fdeb
begfc acbfd gd gbfcade efdabg fdg ecbgdf cbdgf gebcfa egcd | defgbac dgf gdf fgaceb
befacd efbdgc gfcabde ade gfadce da bdeaf ebgaf bfdec dacb | dbafegc cabd dea edafb
cfd abgfc befgcad agdf dcefgb dfacbg abgefc bfdac bcdae fd | afgd badfegc cbeda cfd
gcbefd bcgfa ad dgae fedgab dba fabgdce gafbd fbgde fdceba | afdbg da fgbad ecfdgb
dabc bdcefa acebf bfcagde cb cfb fbaeg gdefbc caedf dcgaef | fbega fcb cb cb
beadc gdafce ea dacfb ace fgabdc cdgbe dbacfge eadfcb ebfa | ae aecfdb bgacdef beaf
dgf ebgcad cbdfag gf bfgde cfgbdae dfbea cfge gbfecd bcged | cegbfad cfeg dgf gfd
fadeg abedfc abgdfec efg cgefba decfa fgadb cgedfa cedg ge | gedc dcge gefda gefbca
abfecd dcegaf caebfgd gaf bcaeg gcfae defagb dcgf dcfea fg | fg afg beacgfd faebgcd
acdbg cbgead efbgdc acfd cbdfgae bdfag cbafgd fgbea df dgf | begaf fgd fdac fgd
dc cfd badgfc fbgda acdb agdbfe fgcda gdfecb fegacbd fcega | dcf fgbeda cdfaebg cdf
adg geab cgbed dbgca debagc dfabc cebgdf ag eadcbfg adefcg | bcgda bgeadc ag bega
fge fbgced efbcg cafged fe aegbdfc edgcb gecadb fbed fbgac | gfe cebgd agbdcfe gfe
cgfabed ge egf fabged agcbf adge ebafd cfbgde dfbcae abgef | gead feg egfab ebgcfd
faecdbg ecadb fbagce cbf fb acgef agfdce fcadgb cabfe befg | befg fbge fb gfadce
bdagce cedfb dfac bac gfcdbe cdegfab cfeab abegf ca caedfb | afgedcb acgdfbe bca dfcgeb
fcbda fgd agecdf bcedgfa dbag dg fgcdb bacfdg ecfbg bacefd | bdfca fcadb bgefc dfg
fegabcd efagd bdcega dc geacdf gfbca fgacd dgc fdeagb fdce | fced gdc fcgadbe cdg
daecb dacefbg gcabde cfaed df fcbd bfgade aecdfb geacf dfe | bcfd adgefbc fd bcfd
gabecd df begad fcbdeg dfab efbdga fdg gdeaf acfeg gafbcde | df badfeg fd fdgcbea
efcag fegda ec efc gafdeb afbcg dbcgef agbdfec fedgac eacd | cfe fdgbec cgfedba ecf
fegcdb gecfda daebcf ca bagfecd dcfbe adc dgbfa bfcad beca | baec adc ecgbafd acd
geacdf fgb fgabdc bfcgd edfcabg gdafc defgba bf bcedg fbca | aecdfg fcdga agdbfce gbf
edabf aceb bcadef ecfdb eagdf gadfcb dbfcgae ebcfdg ba dba | cbae abcfgde dcbef ecafgdb
dgfecb efgdb cbe gdec gbfac bcdafe fbcdeag cebgf eabgdf ce | ce cdbeaf edgc eafbcdg
cegfd dbcfg acdebf bdeg bcfdge ecfabgd defacg fbd cfbag bd | ecbdaf cdebfa fdecag cdgfe
agcf cegfda ca ebfda bgcdfe fecda egfdc cbgfead adc dcegba | adc cfagbed afcg bdeaf
gcabd gabefc dbfeag cbgedf de fdaebgc efcd bed cdebg bcgef | baecgf cbdga defc de
fcgbae gedc fdbgac egcdaf gdefbca dbeaf dgf gd acgef deafg | gfd dfg gcde dbefa
efadc fedgcb egf dgfabc bgae eg bdfage fagbd dgafe bgdafec | gbea agbe dbgecfa geafd
deabcf bg gfcbaed gacfb gcb fdacgb afceg gdcefb cfdba dagb | fbdagec bcg cdfabge bcg
cfba gdeab cfdga fbd cdgfeb gecdbaf dcagfe bf facdbg fbdag | acbf fgdab abdge facb
fagdbec dgc fgcb dgeaf fcbeda cebgad dgefc egbfcd gc fcebd | egcfd gc facdgbe cadebf
cfgbae cbgdef egfcb ecfgbda fcagb bagedc gdafb ac abc cefa | daebfgc degacb gcebfad acfe
edgac bdfeca acg baecd cdagefb ecfdg geacdb eabfgc gabd ag | abdg agc cebfad adgb
fdgcae cd bcefa cedg adgbef cdf fdeag dbfgca facde gefadbc | dcf geafd edcg dceg
gbead cb aecbd ebdfcag bcd fadce geacbd ebfgda bdfceg acbg | bdc egbdca dagbe cdb
dgeac feacdg fdea cda fgadcb gfcae ecgfab fceagbd da edgbc | acfebdg da fcabdeg da
fdbeagc cge dabegc geacd cg bfdecg fecad gbca eabgd fdbgea | cg gc ecg ceg
ac acdeg fedbag facgedb cfedg edbga cedgab dabc fbgcae ace | fgdecba badegc dacb dacb
ecfa gcefdb fgecad gadbf adgfebc gcdfa bagdce ac acd gdfce | adc ac fbdaecg fgacd
ebcad bfdagec abgf dfgcb af cegafd bfdca gfdacb adf dcfbge | fcgebad febcdg af bgfced
gfdb dabcg cdgbaf ecdgbaf bfcad deabcf gad gd agfedc bgaec | ebgac gbfd dg gfbd
fdegac gedca dce gadebf cegab cd cbegadf gaefd dcgebf fdac | edfabgc afcegd defcgb gbdcfe
fdebag bedcgaf cadefg fdeb fe fea adbeg cbedag bgfac aefbg | ecdfbga agfcbde eaf bedacg
cabegfd deabg bc cfdb bfcgda afdgec bgc febgac fgacd gbdca | dcabg gbc dgfbeca bgc
dbgfac gc dcg cbafde dgafce aceg fgdeb fegdc edafc dgbefac | fdecagb cg gc cg
afcgd degbac dgcab edgcfba dcefab gecbaf gdeb bg dcbea abg | begd gab bged bg
dfbegca afd afdeb ebfac agbd agdfec bgcefd agdbef bfegd ad | ecfbgd da abcef fbcegd
dbecaf agdfe bgcde dgefba fb gbedcfa feb fgedb cdefga fagb | dgaef afbecd bcdge efgdabc
fgcd beafdg gdfbcae becgd gfb edcgfb abcfe acgbed ebcgf fg | dfbgaec gfdc gfedcba becgd
dacgbf abedg febcdg cbafg ce afedbcg feca gbfaec gebca ebc | ce efca aecgfb cfabg
dagfe cfae fgaced dgfebc fegbcda cf cfgad cdf adbfge dcbag | cafgde cfd fc gefda
bcgdefa dfaceb fbgeac bgcfe ebag dcfeg fadbgc bef bafcg be | fgeabcd be fcgab gecbf
acgfbe cbgda gaebdfc fegd bdfcg df fdb cebdfa fegdbc fbceg | adbfceg dcfeba fdb df
df cgdaf efagc cbfd cegbadf cdbag agdfeb cdbgaf dcbage gdf | dfg fgd fdg dgf
fgcdab dcbafe badfe gf befg gfead gaedc abcegfd fga abdefg | bgadef fdbaecg gf befg
gcedfb ecbagf acbgfd gafe begfc afc dabce cedfgab af eacfb | afge af bfcea baegcf
gfcdaeb ecbfgd gd fbceg dfcba bcdgf afgbce gbd fdgabe dcge | gd gcde aegfcbd ebfgc
gbfade befcd bfagec bdaef daebcf cbfgd ec ceb ecad edabfgc | ce ceda fdbce ec
ceagdf gfbae gecfb dabg cedfba abfdecg deafgb afedb ga afg | bagef fdegca ebfcgad decfba
baged fgace cdeb efdabg abcdfg cb abcegd agdfbec abc egacb | bced abc bac fcaeg
bedcfg gfbedca egdfca cadef dac da gbaced feabc adgf cefdg | gdecbfa dabgec cad abecf
cebdag dfgc fc efc caedgfb gcead ecgfda ebafg fegac bacefd | afegb cef ecfgbda gecfabd
afge aebcg eg dfcbae gce bcefa agbcd gcdebf cebagf abfdecg | geaf ceabg abfce gafe
gdcebfa cgeadf cgd gd fdag befcdg fecda bdcafe cebga gaedc | febdcg gd bdefcga fadg
afdcgb gadbfe cebda gacfbde ecdf edb ed agceb bcafd dcfabe | bgcea bde dfec acfgedb
ceabg cagbfd adbcg adcegb dcbe acdbfeg geafbd eb bae cgfae | be bae fecag agcdb
eafcdg de fadgcb bdafe gbfae bdfca abedcf dea aefdcbg becd | afdbcge eda ed aed
fdecg dgeaf fgc ebgfdca gedfba egac cdgaef dfgcab fdebc cg | acge gace cg edgfc
gf dcgae cdabeg gafd gefadc cgeaf cabfe degcfba gfc bcdgef | gfc ecdgafb adgf dgeca
efbcd cafegb fb gedfabc bcf cedfgb fbdg cfeda cdgeb cbedag | cbf fcb bgced bfc
afcbe aedbf aebfgd bcaeg ecf fc cafd eacfbd gfdecb bgcafed | bedcgf cfad ebacg dfgcbe
dcg cgbed dbfcag abcedf gcae fedcabg egfbd eabgcd gc aebdc | cgd ecag dcg cg
fcabged abgdc gac bgafcd gdfab gabefd cdeab cg gadfec cbfg | edcbafg acg dbacegf gbfc
cdf feacb dfbac dbfcga bfgda adgc dc fdebga gcefdb dbaegfc | gbeafd fabdcg dcf cd";
    }
}