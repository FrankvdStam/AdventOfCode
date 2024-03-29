using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day10 : IDay
    {
        public int Day => 10;
        public int Year => 2021;

        private List<string> _incomplete = new List<string>();


        public void ProblemOne()
        {
            var lines = Input.SplitNewLine();
            var score = 0;
            foreach (var l in lines)
            {
                if (TryFindIllegalClosingDelimiter(l, out char delim))
                {
                    score += _scores[delim];
                }
                else
                {
                    //For part 2
                    _incomplete.Add(l);
                }
            }
            Console.WriteLine(score);
        }

        public void ProblemTwo()
        {
            var scores = new List<ulong>();
            foreach (var l in _incomplete)
            {
                scores.Add(Complete(l));
            }

            scores = scores.OrderBy(i => i).ToList();
            var finalScore = scores[scores.Count / 2];
            Console.WriteLine(finalScore);
        }

        private ulong Complete(string input)
        {
            var count = new Dictionary<char, int>
            {
                ['('] = 0,
                ['['] = 0,
                ['{'] = 0,
                ['<'] = 0
            };

            var depth = new List<char>();

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                var currentCount = 0;

                if (_closeToOpen.ContainsKey(c))
                {
                    depth.RemoveAt(depth.Count - 1);
                    count[_closeToOpen[c]]--;
                    currentCount = count[_closeToOpen[c]];
                }
                else
                {
                    depth.Add(c);
                    count[c]++;
                }
            }

            ulong score = 0;
            depth.Reverse();
            foreach (var c in depth)
            {
                score *= 5;
                score += (ulong)_scores2[c];
            }
            return score;
        }


        private bool TryFindIllegalClosingDelimiter(string input, out char result)
        {
            result = '\0';
            var count = new Dictionary<char, int>
            {
                ['('] = 0,
                ['['] = 0,
                ['{'] = 0,
                ['<'] = 0
            };

            var depth = new List<char>();

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                var currentCount = 0;

                if (_closeToOpen.ContainsKey(c))
                {
                    if (depth.Last() != _closeToOpen[c])
                    {
                        result = c;
                        return true;
                    }

                    depth.RemoveAt(depth.Count-1);
                    count[_closeToOpen[c]]--;
                    currentCount = count[_closeToOpen[c]];
                }
                else
                {
                    depth.Add(c);
                    count[c]++;
                }

                if (currentCount < 0)
                {
                    result = c;
                    return true;
                }
            }

            return false;
        }
        
        private Dictionary<char, int> _scores2 = new Dictionary<char, int>()
        {
            { '(', 1     },
            { '[', 2    },
            { '{', 3  },
            { '<', 4 },
        };

        private Dictionary<char, int> _scores = new Dictionary<char, int>()
        {
            { ')', 3     },
            { ']', 57    },
            { '}', 1197  },
            { '>', 25137 },
        };

        private Dictionary<char, char> _closeToOpen = new Dictionary<char, char>()
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' },
            { '>', '<' },
        };


        private const string Example = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

        private const string Input = @"<<{<[<<<{<<(<({}())({}[]))[[{}[]]<{}[]>])[<[{}[]]{()()}>{((){})<<><>>}]>>[<(<<{}>>[<()()><<><>>]
{[[[{<[(([[{(({}))([<>[]]((){}))}<<<[][]>[{}{}]>[{()[]}(()())]>][[[{<>()}[{}[]]]]]]<{{{{()[]}}}[[{[][]}{{
([((<(<(<<([{{[][]}([]<>)}({<>{}}[[]()])]<[[()()](()[])]([[][]](<><>))>>{{<(<><>)[<>()]>([[]()])}<<[[]()]<[][
{{{([((([({[{{()()}{<>[]}}({{}{}}([]{}))]{([{}<>])<(<><>){[]<>}}}})({{{(()())[[][]]}{<{}{}>
{{([[((({(({({<><>}([]{}))([()()]{[]{}})}[[({}{})<(){}>]<<()<>>>]){[[[()()][<>[]]}<[{}()]{{}<>}
{([<[[<<({[<(<()<>>(()())){<<>[]><()[]>}>[({{}<>})]][[(<()<>>{(){}})[{[]<>}[[]{}])][[(()()){[][]}]<{{}{}}<[
[<{[((<([[([<<{}()>{{}()}>{(()[])[<>{}]}]<{[<><>]{<>{}}}>)]{[{{{()<>}[{}<>]}<<()>{{}{}}>}]({{<
[<((<<({[{<{[[<><>](<>{})]{<()()><[]{}>}}<<[{}()]<[][]>><({}<>)>>>{<[[()<>]<{}[]>]<(<>()){<>{}}>>[{(()[])[{
([(<<{{<[<(<<[[]{}]({}{})><[()[]]{<><>}>>[[{[]<>}[{}()]][({}{})({}())]>){(<{(){}}[{}[]]>[{[][]}<<
([<{<<<[[<{[{({}{})[{}[]]}])[[(({}{}){()<>})<[()[]]<[][]>>]<(<()()>{()()}){(<><>)([]())}>]>]]<<{
[{((<<{[{[<<<(()[])[[][]]>>><{<{<>[]>{[]{}}>{<<>()>[<><>]}}[{{<>{}}([]())}{({}[])[<><>]}]>][(<({<>()}[[]{}])[
<(<[[<{(<[(<[([]())]({<>()}({}()))>[({(){}})[[[][]][<>{}]]]){{[[<><>]{{}[]}]<[(){}]<{}()>>}(<(<>{
{{{([[(<<<{[{(<><>){<>[]}}<<[][]><()<>>>]<[[(){}][<>[]]]{{()<>}[{}()]}>}[{{({}<>)}[[{}{}]{{}[]}]}[([()()]
{(<[{[{([({[[{<>}]{[<>{}]([][])}]{[(<>[])({}())]<{<><>}({}<>)>}}<[[(<>()){()[]}][<()()>{[]{}}]]<{[[]][[]()]
<{(({({(<{{<{{()()}<()[]>}{[[]()](<>[])}>}<<({{}()}<[]{}>)[{[]()}]><<<<><>>[<>[]]>>>}([<[{()[]}(()()
[[[{<({{[[<[{[(){}]<[]<>>}[[<>[]]{()<>}]]({<{}[]>[<>[]]}{({}{})<{}[]>})>{[[{[]{}}{<><>}]{(
<([<[(([[[[([<[][]>]<([]()){<>()}>){[{[]()}([][])]<{[]<>}({}<>)>}]<[[[[]<>][<>{}]]{{{}()]{<>(
<({{<<<{<<[[[<()<>>[()[]]]](<[{}<>]{[][]}>[{<>()}<()()>])]><{<[<()<>>[[]()]]<<[][]><()()>>>
[[{{(([[<{[[(<[][]><<><>>}[{{}()}]]{(<<><>>({}<>))[[[][]]<[]{}>]}]<[{({}<>)[[]]}({()()}[<><>
([<[([[<{<((<<{}()>[<>()]>)(<{{}()}>(<[]<>>{()()})))>({<<{(){}}{{}}><[<>{}]<<>{}>>>{([(){}](<>)){{()<>}({
[{[[<((({[{[<[[]()]<{}[]>>{<{}<>>(<>[])}]<{({}{}){{}[]}}{[[]<>]([][])}>}(<{(<>)[()<>]>>(({{}()})[[{}[]]{[]}
[{[[({<<{([{{({}())[()[]]}<[<>()]({}{})>}({[{}<>]<<><>>}{{[]<>}{()]})][<([<>()]({}[])){[{}{}](<>[])}>[((()
{{((<[{{[[(<{({}()){()<>}}[{{}<>)({}{})]>({<(){}>[[]{}]}[[[]()]<{}>]))<<{[[]{}]({}[])}{<()>({}
<{<({[[({<(<<<[][]>[<>[]]><(()[])>>{([[]<>]({}{}>)})((<[{}][{}<>]>{(()<>)})<<<<>[]>[(){}]>[(<>{})<<>[]>]>)>}
[[[{[[{<{({<({{}<>}({}<>))>{[({}())]}})[<[[([][])][<{}<>><()<>>]]([[<><>]<[]()>]<({}{})(<><>
[<({{[(<{(<<((<>)<{}[]>){{<>[]}<{}{}>}>(<(<><>)<{}<>>><{{}()}[{}()]>)>([([[]{}][{}[]])]))}>((({{
<<{({{(<([[(<[<>()]<<><>>>(<[]()>{{}<>})){{<{}[]>[()()]}<(<>[])<()[]>]}]{({<(){}>{[]()}}[(<>
(({<[({{[[{{[[()[]]([][])]{(<>[])<(){}>}}<<({}{})([]())><({}[])>>}<<<<{}[]>{[]{}}>(<[][]>[{}{}])>(({()<>
{[[([<[{[{<[[({}[])<{}[]>]([<>[]]({}{}))][({()[]}(()()))]>{<<({})([]{})>{[[]()](()()]}>[(((){})(()[]))<[[]<>
({[(<[(<({<[[[{}()]][<()()>[()[]]]]<{{<>[]}}[[[]{}]([]<>)]>>})<<{[<[()[]](()()}><(()())[<><>]>
((<[{([[{(([([<>()][[][]])]))}]])<{(<<{{<<[]{}>({}())>}}>>[{([[{{}()}{(){}}]](((()[])[(){}])<[<><
{(<{[{<<(<<[<<{}{}>><[[][]}{(){}}>]{{{[]<>}[[]<>]}}>{({<{}<>>({}<>)}<<[]<>>>)[(<{}<>>(<>))<[
[<({<<({[{[<[([]<>)]{[(){}]({})}>{{((){})}[<[][]>]}]({(<{}<>>(<>())){{[]()}(()())}}(((<>{}){()()}){{[]()}<<>
[[{{[<<[[[<<[{{}[]}<(){}>][{(){}}<{}[]>]>>(({({}{}}{<>{}}}([<>[]](()[]))))]{[[{((){})<<>()>}{{()[]}{<>()}}]<
{(([([<({[[{{[<>[]][<>[]]}[([][])[{}()]]}[([()()][{}()])([<>{}])]]][[{[([]{}){[]()}][<(){}
<{{[{<(((({[{<[]()>({}{})}]}))<[((<([]<>)[{}[]]>{<(){}>[[][]]}))[{<[()()][{}()]>(({}[])<<>[]>)}[((<>()){{}
[(<[{[[<{[([{{[]()}<()>}(([]())<(){}>)][(({}{})<{}<>>)])(<({{}()}({}))<[()[]]{<>[]}>><(<{}<>><{}<>>)<({}<>
{([({<<((({[({[][]})<{{}{}}<{}()>>]}<(<<[]()><()<>>>{{[]<>}([][])})(<{[]<>}[[]<>]>)>){([{<[]()>[<>[]]}[{<><
[{{<<[<<<<{<({[]}[()[]]){{{}<>}[(){}]}>({([]())({}[])>)}><[[{[()<>]({}{})}<<<>{}>{()[]}>]{{[[][]]}(<<>()><
(({({[{({[{[{([][])[[][]]}]{{{[]()}({}[])}({<>[]}(<>))}}[<<([]<>)>(<{}{}>{()})>[{[<><>][<>{}]}]]]}([{
(({{<[{[([({([{}{}][{}<>])<<{}{}><<>()>>})]([((<[]()>[{}()])<<{}<>>((){})>)]<{[[{}<>]<[][]>]<(()[]
{<[({<[[<[(<<({}[])[[]()]>([<>{}]{{}<>})>[<<{}[]>{<>()}><[<>{}]({}[])>])]{{{({{}{}}<[]()>)
{<(({<[({<<[[[()[]]<{}<>>]]>><[{<{<><>}(<><>)>[[()[]]{<><>}]}[[{<><>}{{}<>}]({{}{}}[[][]])]]<<[{{}{}}<<>[]>]
[<{({{(({([{(<{}[]>(<>{}))<[[]{}](()[])>}<{(<>{})(<>{})}>])<<<<({}<>){<>{}}>[(()()>(()())]>
({((({[(<{{({({}())[[]<>]}([<>[]]{{}()}))<({{}()}<{}{}>)[{[]{}}({}[])]>}[([{{}<>}<{}>])[{{()[]}{{}()}}[([]{
[<[{<{([<({<[[{}<>]{[][]}]{<<>{}><<><>>}>({[[]{}]<{}<>>}{{{}<>}(()[])})}){{{[<[]()><[][]>]
<<{<{[[{([{<[<{}[]>{[]<>}]<<{}{}>[{}[]]>>}([([{}{}]{<><>})[{<>{}})])]){{([[<{}[]>({}[])][[<><>]
([[(<([[[<[((([]{}){[]<>}))<{<[][]>[[]()]}[(()<>)<[]{}>]>]{[([[][]][()[]]){{(){}}[{}<>]}]}><
<[{{<{<({{([[[[]{}]<{}()>><([]()){[]{}}>]<{<()[]>([]())}>)}<<{{((){}){[][]}}(({}<>)(()()))}([{[
({{({[[{<({[({<>{}}[<>()])(([][]){{}{}])][({{}<>}(()[]))[<[]()><[]{}>]]}[{(<()[]><[]()>)[<{}()>{()[]}]}
<<<{<{[([[[[(<()[]>){<[]()><[]()>}]<<([])<[]<>>><[[]<>][<>[]]>>]<<[[()()](<><>)]({()()}[<>[]])>([<[]
((([{([({({<(<{}()>[()<>])<[{}<>]<{}[]>>>][{<[[][]]{{}()}>[({}()){[]<>}]}([{{}{}}[[]()]])])<[(({<>()}
([<<[(<{<<[<[{()[]}{()[]}](({}[]){{}[]})>]{([{()[]}])({{<>()}(()())})}>((({{()()}(()<>)}][[(<>()){()}
({[{[((<[<{<([[][]]{{}()})[{<>()}(<>{})]>{{{{}}}{{()}({}[])}}}>]<(<{<(<>())(()())>}>[<{[{}<>]{{
{({[<{<[[[[<({<>{}}{()[]}){<[]()><[]()>}>([(<>())[[]()]](({}{})[()<>]))]]({([[<>{}]<{}[]>](<()()><()<>>))
([<[<{<<<<((({<>}[<>{}])<({}())<()<>>>)[[[[]()][{}{}]]<(()<>)[<>]>]){{[({}())][([][])<()[]>]}{
[{[({{<[{[<<[[<>()]]<([]())>><<<[][]>>{[()<>]{{}}}>>]}]{<{{{{[<>{}]({}[])}<{{}}{<>{}}>}{<({}){<>
{<<{{({([{[([{[]<>}{<>[]}](<()[]>))[([{}]<()<>>)<<<>[]>[()<>]>]]}{[{<[()[]][{}{}]>{[[]()][{}[]]}}({[[]{
((<(<{{<([{[{{()[]}{<><>}}{{<>}({}{})})[([(){}][[][]])[<()[]><(){}>]]}<([(<>()){[][]}][([]())([]())])[
<<{[{{<{<[[[<<()<>>[()[]]>([()]{{}[]})]](<[(()<>)[()[]]]>{<[{}{}]>[<<><>><<><>>]})][{((<{}{}>[()
<<<[[<[[([{[(<()><[]{}>)[(()[])[[]{}]]]{[{{}<>}[{}<>]]}}[(<<<>()><()[]>>[({}())<(){}>])[[[[][]][<
[[<(({[(([{{<([][])[[][]]>[{{}{}}{<>[]}]}<[(<><>)](<[][]>[<>{}])>}]{{{([(){}]([]))<[{}<>](())>}([[(
[<{[([<{({([((()>[{}[]])[(<><>){[][]}]])([{([]<>){[][]}}<(()[]){[]()}>]<((<>{}){[][]})({{}()}<()[
((((<<<({([<{[{}()]{[]{}}}{({}[]){(){}}}>[[{()[]}[<>()]]<(()<>){(){}}>]]{[<(()())[{}[]]>(<<>()>({}
(<({((((<{{{[{{}{}}[(){}]][{<>{}}{(){}}]}([[{}[]]{<><>}]<[[]{}]<[][]>>)}({[[{}<>]<(){}>]}<{{<>[]}}<<<>[]>
{(<{(<{({(([{{()()}}][(({}{})<{}>)])<<(<()<>>{{}<>})(((){})[[]{}])><[[<>{})[<><>]][{()()}]>
<{[(<[[{(<({[<{}()>{{}{}}]{(()){[]{}}}}[{{[]<>}[{}{}]}])({({[][]}{[]()})})>(<{{(()[])<<>>>[<<>[]><[]<
([<[<[[{<[<(([<>{}]){[(){}]<()()>})<{([]{}}<{}<>>}{<{}{}><[]()>}>>(<<{<><>}(<>())>({<>}[{}{}])>(<(<><>)
([{[[(<[(([{[{<>[]}{<>()}]<[(){}](<>{})>]<<<<>[]>>{(<><>)<[]{}>}>])){{{{<([]<>)([]{})><{{}{
[<{({{(({<[([[()<>]{[][]}]({<>[]}(()[])))](<<({}())([][])>[{()<>}(()())]><{[<>{}]<<>()>}[[{}()]{<><>}]>)>{
{({{{{[{{[<{{<[][]>([])}<[[]{}][{}()]>}>]{{[[<<>[]>[[][]>]<<{}<>>(()<>)>]{[[<>{}]{{}[]}]}}<[[[<><>]<<
<({{({(({{<{{([]{}){<><>}}<(<>())({}{}]>}[{((){})(<>())}]>(({<[]()>([]{})}({{}{}}<<>()>)))}[[([<{}<>>
[[<{<{<((<<({(<>())(<>())}{{(){}}[{}()]})<(<{}<>>{(){}})>>{{({[][]}{{}[]})<<{}<>>(()<>)>}<<<<>()>>{({}{}){()
{[(<<<{(<{<<({<>{}}{()[]}>({<>[]}({}()))>>}>)<{(<(<({}[]){()[]}>)>((({()[]}[[][]]){<[][]><<>()>})[(([]<>)<
<[<[{<[{[[<([<{}()>[<>]]{[[][]]<{}()>}}>({{{<>[]}{()[]}}{{()}<<><>>}}<<([])([][])>{[{}<>]((
{[<<[[(<<{[<[[[]]({}())]([{}()][[]<>])>]}<({{(()[])<[][]>}{({})<()<>>}})([((()[])<[]<>>)<{(){}}([]())
<{{<[[[(((<[{({}()){()()}}(<{}()>{()<>})]{{{()()}}{[[]{}]({}())}}>))<{<(((()<>)[{}[]])){[<[]<>>{{}()
<([((<<(((({{[{}<>]<[]<>>}(<[]{}><[]{}>)}[{{<>[]}}{(<>{}){[]<>}}])<<{(()())<<>{}>}[[{}[]][{}()]]>[<[[]()
{({{({[[({[<[<<><>)<{}{}>][<(){}>{[]{}}]>({(<>{})(<>())}<({})({})>)]})]<({[[({[]<>}{()()}){({}()
((([[(<(<[([({{}()})[{{}()}({}{})]]{({[]()}{{}()})(<()[]><<>[]>)}){{([{}()])}}]<[(<(<><>)[()[]}>[{{}<>}])]>>[
{(([(<[[[[<<([(){}][<>[]]){({}())[[]{}]}>>[[<{()[]}{(){}}>(<{}{}><{}{}>)]<([()[]]<{}>)(<()[]>{
{({([(<{<[[<<[<><>]<<><>>>>[<<{}<>>[{}<>]>{[[]{}][{}<>]}]]][{{<[{}[]]({})><[()()]>}}<[<{[]<>}[[]()]>]{[
(((<{([<[(<[{([][])<[][]>}({{}<>}[{}{}])]>[[({[]<>}{<>{}})]})[{({(()[])([][])}{<()()>[[][]
<(<{{<([<(<{<{{}}{<>{}}>}([([]{}){(){}>]{<()<>>([])})>)<<({<[]<>>})({[()[]]}<[(){}]{{}()}>)>{[<<[][]>>({[]{
({{(<(([[{((<<()[]><{}<>>>])}][<({<[{}[]][{}{}]><(()())([]())>})>{<(<{[]{}}{{}<>}>{{[]<>}{<><>}})>}]]{<[{[[
{<[<([(((<[{[(())]}[(<{}()>)([[]][{}])]]{({<[]{}><()[]>}{{[]()}<{}<>>})}>){(({{<(){}>{<>[]}}(<(){}>(
([[{<{{{({{<[<[][]><()()>>><<[<>()]>>}{[[(())]<{[]<>}<()<>>>]}})(<({[([]<>)<<>[]>]({<>()}[
{<[<<[((<<{<{([]<>)(<><>)}([()<>][{}[]])>}<<{{[]()}(<>{})}([<><>]({}[]))>{<[()<>]<{}()>>{<()(
{<{(<[[[<[{<[({}())<()<>>]>[{([]{})([]<>)}[<[][]>]>}][<([[()<>]{[][]}]){<[[]()](()())>({()}(()()))}>({<[()<
[{({<<<({{<[(({}[]){{}()})[{[]{}}{<><>}]]<{<[][]>(<>())}([<>{}])>>}[(<{{<>}([][])}>{{[[]()][[][]]}{<<>()><
<<([{{[(<[([<{[]()}<<><>>><(()())<()[]>>][{<[]()><{}()>}{<<><>>[()<>]}])(<<<<>{}][{}[]]>[{<>}({}
((<<<<[{<[[<[{()()}<[][]>]{(()<>)}><{[[]{}]<{}{}>}>]<<{{[]<>}([])}{({}())([]{})]><[(<>())(<>{})]>>]><([<(([]
{[[<(<[({[[[{{<>()}<()()>}]]]}[({(<[[][]]>(<{}<>>))}[<{<[]{}>(()<>)}{<{}{}>[()<>]}>])(<{([[]<>]){
{{<<{([<{[[[(<{}{}>[[]<>])({<><>}{{}[]})]]][([([()[]])[(()<>)<[]{}>]]<{{()<>}{[]()}}<<[]{}>{
<(([[<(({{(<{{()<>}[<>()]}[({}())[[][]]]>{{[{}<>]((){})][<<>{}><[]{}>]})<<({{}()}<<>{}>){(<>())[<>{}]}>(
(<[(<<(((([<[<{}[]>][[()()]<<>()>]><[<{}{}>{()<>}](<()()])>][{{[{}<>]{<>()}}[<()()><{}>]}<(<{}{
({{({[(<[{[<[((){})({}{})]([<>[]][[]()])>](<(<(){}><()()>)({<>{}}<{}()>)><{{<>()}(())}<{()()}<[]()>>
{<([(<<{([[[[(()<>)(()())]][[([]{})({}())]<[[]<>]{()[]}>)]([[<(){}>{<>{}}]((()())[()()])])]
(<<(((<[[{[{<[()<>]{<>{}}>([[]()][()()])}]{([<[]<>>{<>[]}]<<[][]>[{}()]>){<{[][]}[<>{}]><[<>[]]({}{}
<([[[(<[([{((<<>()>{()[]}){(<>())([])})[{<{}[]>([][])}[({}[])[<>()]]]}[(<<<>{}><()<>>><<<>[]>{(
[[{<[[(<<<(([(<><>)([][])](([]{}){[]{}}))[<[[]{}]>([[]()]{[][]})])>{{{[<()[]>[[]{}]][{()<>}[<>[]]]}}
([(<{<<(<{{{[[<>[]]<(){}>]([()[]][()[]])}{((<>()))}}{<[<{}<>>(<><>)](([][])<<>()>)}}}(<<[<{}[]
<[{{({{<[<[{(<<>[]>(<>()))<([]<>)[{}<>]>}]>{<<{<[]{}>(<>())}>([<(){}><(){}>]{[<>()]([]())})>}]>}})}}{(((
[<(((<{<<[(<{(<>[])}>){(<<{}<>>([]<>)>{<{}()>}}[[<{}{}>{[][]}](<<>[]>{{}[]})]}]><<([<[(){}]<()>><[(){}]
({[[(<[{(<((<[()[]]{[]<>}>){<(<>){()<>}>})(({<(){}>[{}[]]})[[(()[]){[]<>})<{[]()}>])>)<<<(<({}())(<>[])
[{([<<<[{{([<<{}{}>>[{{}[]]]])}<<[<{{}}{<><>}>(({}{})[<>[]])]({[<><>]([][])}{(()<>)<<><>>})
[[<(<<[{([[[[<[]{}><[]()>]][{[[][]]<()<>>}]]]{{{(<[]{}>(()<>))<(<>[])<<><>>>}]({{(()[]){{}()}}
({<<<{[<[[[{([{}<>]{<>()})(<<><>>)}(<(<>[])<{}>))][({<{}[]>[[][]]}(([]())(()[])))<<[[]()]{<><>}>>
[{<[[{{[<<([([<><>]([]<>))]({[()[]][{}{}]}{{()()}[[]<>]})){(<(()[])<[]()>><[[]()]<()<>>>)(<<()(
{<({((<([<(({{[]<>}[{}<>]}[<()[]><[]{}>]){<{[][]}{<><>]>})(({({}())[()()]}{[{}()][[]<>]}))>])<(<{(<{";

    }
}