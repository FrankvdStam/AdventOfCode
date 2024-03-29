using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day08 : IDay
    {
        private class Instruction
        {
            public string MutRegister;
            public bool DoIncrement;
            public int Increment;

            public string TestRegister;
            public string Operator;
            public int Number;

        }


        public int Day => 8;
        public int Year => 2017;

        public void ProblemOne()
        {
            var instructions = ParseInput(Input);
            int max = Int32.MinValue;
            var registers = new Dictionary<string, int>();

            //Init all registers beforehand to save hassle later on
            foreach (var instruction in instructions)
            {
                if (!registers.ContainsKey(instruction.MutRegister))
                {
                    registers.Add(instruction.MutRegister, 0);
                }

                if (!registers.ContainsKey(instruction.TestRegister))
                {
                    registers.Add(instruction.TestRegister, 0);
                }
            }


            //Interpret the things and stuff
            foreach (var instruction in instructions)
            {
                if (ExecuteInstruction(registers[instruction.TestRegister], instruction.Operator, instruction.Number))
                {
                    if (instruction.DoIncrement)
                    {
                        registers[instruction.MutRegister] += instruction.Increment;
                    }
                    else
                    {
                        registers[instruction.MutRegister] -= instruction.Increment;
                    }
                }
                var temp = registers.Values.Max();
                if (temp > max)
                {
                    max = temp;
                }
            }

            _cashedResult = max;

            var result = registers.Values.Max();
            Console.WriteLine(result);
        }

        private int _cashedResult;

        public void ProblemTwo()
        {
            Console.WriteLine(_cashedResult);
        }


        private bool ExecuteInstruction(int registerValue, string operator_, int number)
        {
            switch (operator_)
            {
                case "==":
                    return registerValue == number;
                case "!=":
                    return registerValue != number;
                case ">":
                    return registerValue > number;
                case ">=":
                    return registerValue >= number;
                case "<":
                    return registerValue < number;
                case "<=":
                    return registerValue <= number;

            }
            throw new Exception(operator_ + " is not a supported operator");
        }


        private List<Instruction> ParseInput(string input)
        {
            List<Instruction> instructions = new List<Instruction>();

            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
                Instruction n = new Instruction();
                n.MutRegister = bits[0];
                n.DoIncrement = bits[1] == "inc";
                n.Increment = int.Parse(bits[2]);

                n.TestRegister = bits[4];
                n.Operator = bits[5];
                n.Number = int.Parse(bits[6]);

                instructions.Add(n);
            }

            return instructions;
        }


        private const string Input = @"d dec 461 if oiy <= 1
phf dec -186 if eai != -2
oiy inc 585 if lk >= 9
bz dec -959 if gx < 9
vyo inc -735 if hnh > -7
bz inc 329 if hri < 6
vyo dec -425 if pce == 0
rjt inc 668 if x >= 2
bz dec 602 if hri != -10
phf dec -169 if aam != -2
d dec 266 if n >= -2
pce inc 907 if d > -734
zbh inc -345 if vyo <= -317
gx dec 809 if sy != -4
fu inc -127 if lk > 8
yl dec 166 if fu != 0
hnh dec -33 if d <= -721
x dec -372 if hk > -2
oiy dec -140 if kp < 1
aam inc 702 if d != -735
fu inc 888 if phf != 363
phf inc -692 if oiy > 138
phf dec 247 if hri < 9
pce dec 370 if q == 0
wez inc -919 if eai <= 0
oiy dec 89 if hk < 8
eai inc 600 if x == 372
kp inc -340 if n < 9
oiy dec 251 if ey <= 8
pce inc -315 if zbh <= 3
x dec 682 if hri < -6
fu inc 561 if x > 362
gx inc -202 if hk == -5
vyo inc -184 if eai == 600
pce dec 954 if aam < 709
phf dec 702 if zbh >= -4
aam dec 640 if fu <= 1455
x dec -119 if lk == 4
gx inc -722 if rjt == -10
pce inc 231 if bz <= 692
hri inc 402 if q >= -8
d inc -759 if vyo == -494
n inc 358 if x == 372
eai inc 56 if lk != 5
ey inc 268 if yl < 7
vyo dec 828 if q < 9
yl inc 320 if eai >= 655
aam inc -262 if hri > 393
bz inc 445 if n <= 359
yl dec 327 if px != -9
d dec 735 if hnh != 33
rjt inc 8 if zbh > -3
hr inc 347 if q > -6
oiy dec 573 if ey >= 265
zbh dec 696 if bz >= 1137
zbh dec -292 if eai != 655
ey inc -205 if d != -1492
rjt inc -538 if px != -1
kp dec 511 if zbh > 285
fu inc -19 if hnh != 41
zbh inc -996 if rjt < -525
yl dec -28 if px > -4
d dec 341 if sy == -2
oiy dec -799 if pce >= -500
pce inc -795 if pce > -505
n dec 559 if n != 363
pce dec 814 if ey == 63
hri dec -664 if px != 0
bz inc -885 if vyo > -1327
hr inc 454 if wez <= -923
hk dec -721 if hri < 405
eai inc 168 if eai <= 664
ey dec 261 if phf >= -1287
hnh dec -59 if phf < -1284
n dec 81 if oiy < -769
lk inc -714 if zbh != -713
fu inc 819 if ey >= -198
oiy dec -155 if pce <= -2108
oiy dec -598 if wez > -915
gx dec 397 if pce == -2115
px inc 934 if fu > 2244
fu inc -916 if yl != 22
wez dec 428 if kp <= -846
gx dec -15 if px <= 941
wez dec 858 if sy > -1
lk dec -875 if zbh > -708
hk inc 575 if q <= 2
rjt dec 734 if hk <= 1300
lk inc 399 if wez <= -2201
x dec 485 if lk == 560
pce inc -273 if hk != 1302
sy inc -962 if hnh <= 98
aam inc 228 if pce != -2381
wez dec 55 if x >= -116
eai inc 954 if hnh != 96
phf dec 799 if oiy != -610
q inc 95 if oiy == -618
vyo dec -595 if pce == -2383
pce dec 258 if kp < -848
n inc -730 if bz < 248
pce inc -855 if pce == -2641
vyo dec -817 if vyo <= -723
sy inc -333 if hr != 350
vyo inc 360 if sy <= -1288
q dec 780 if d <= -1480
hri dec -584 if vyo < 455
bz inc 299 if phf >= -2094
gx dec 472 if aam != 30
px inc -126 if sy <= -1286
kp inc 45 if vyo < 454
eai dec -473 if hri != 988
gx dec -534 if gx <= -1260
oiy dec -334 if pce > -3499
lk inc -243 if vyo < 459
ey dec 76 if gx > -736
eai dec 959 if fu > 1332
zbh dec -75 if fu < 1339
wez inc 938 if sy <= -1290
hk dec -605 if sy >= -1285
hk inc 271 if pce == -3495
phf dec 300 if hri != 995
d dec 420 if px < 812
pce inc -799 if rjt <= -1262
fu dec 600 if hk < 1299
x inc 633 if yl == 21
q dec 684 if gx > -739
oiy inc 780 if pce != -4301
x dec 270 if hk == 1296
px inc -580 if gx >= -733
d inc -699 if oiy > 503
wez inc -244 if fu == 733
oiy inc 108 if vyo < 452
q inc 37 if d != -1910
eai inc 875 if kp > -814
aam dec -625 if rjt <= -1266
phf inc 148 if ey == -272
rjt dec -685 if hri != 993
zbh inc -780 if pce < -4285
wez inc 607 if d > -1913
pce dec 315 if fu >= 738
hri dec -270 if sy != -1294
hnh dec 734 if yl != 23
sy inc 344 if oiy == 604
pce dec -181 if eai > 2171
n inc 230 if q >= -1322
sy inc 315 if yl == 21
hr inc 696 if hri < 1259
eai dec -920 if yl != 14
hr inc -876 if yl >= 28
eai inc -18 if rjt == -579
hk dec -204 if fu != 727
hnh inc -866 if wez != -957
n inc -274 if zbh < -1405
rjt dec 379 if yl < 31
kp dec -429 if aam > 26
hnh inc -420 if bz > 542
lk inc 769 if yl <= 30
hnh dec -712 if yl >= 19
oiy dec -412 if rjt < -949
oiy dec -193 if pce > -4299
ey dec 687 if eai < 3072
px dec 464 if eai <= 3078
x inc 866 if hr <= 1051
zbh dec -916 if oiy < 1219
d inc 82 if rjt < -957
hri dec 527 if sy < -642
oiy inc -607 if rjt < -957
hnh dec 859 if hr >= 1040
q inc -498 if x > 1116
bz dec -414 if kp >= -385
oiy dec -115 if gx > -739
oiy dec 570 if gx == -731
gx dec 142 if wez != -957
q dec -118 if zbh != -487
ey inc 335 if pce == -4295
wez dec -86 if lk > 1080
px dec 461 if px == -236
d dec 276 if ey != -622
vyo dec -472 if yl >= 16
wez inc 762 if yl > 14
vyo dec -365 if px == -699
zbh inc -415 if x < 1113
hk dec -155 if phf <= -2386
hnh dec -647 if fu == 727
bz inc -860 if ey == -626
q inc -49 if n <= -1283
hk dec 143 if gx <= -871
hri inc -845 if phf >= -2394
hri dec 261 if hri == 411
zbh inc -289 if oiy != 724
q dec 976 if hnh <= -2075
hnh inc 70 if pce < -4293
hnh dec 965 if hk == 1357
px dec 249 if lk <= 1080
hr dec -631 if aam > 25
sy dec -340 if ey == -626
yl dec 145 if q >= -2246
fu dec 387 if oiy <= 707
yl inc 917 if kp == -384
x inc -871 if rjt < -951
sy dec 553 if vyo >= 914
px dec 303 if px != -688
yl inc -584 if vyo >= 920
vyo dec -216 if zbh <= -785
gx dec -857 if hnh < -2964
vyo inc -384 if wez != -113
fu inc 925 if x <= 247
phf dec -79 if pce > -4302
q inc 487 if vyo < 536
hk inc 551 if hnh >= -2977
d inc -741 if lk > 1082
hnh dec -387 if q == -2239
n dec -466 if hri != 150
hk dec -83 if fu <= 1651
rjt inc -455 if px != -1000
ey dec 672 if n < -1280
d dec -485 if phf == -2306
eai inc -45 if phf >= -2305
lk inc 440 if bz > 92
hnh inc 589 if hr != 1680
vyo inc 55 if pce != -4285
kp inc 480 if x == 245
kp inc 996 if hr < 1677
fu inc -27 if sy != -842
n dec -509 if vyo < 599
sy inc -430 if fu >= 1630
hri dec 755 if q < -2232
q dec 750 if vyo > 584
pce inc -606 if lk >= 1521
hk dec -473 if pce > -4909
pce dec 564 if px >= -1007
vyo dec -156 if d > -2350
d dec -58 if d != -2365
vyo dec 780 if wez == -111
zbh inc 757 if n >= -779
wez inc -82 if oiy > 712
wez inc 266 if hk >= 2380
yl dec 954 if hri == -607
eai dec -902 if px > -1004
q dec 386 if oiy < 726
q inc 221 if sy != -1270
n dec -10 if wez != 82
fu inc -986 if rjt <= -958
hri inc -913 if bz >= 96
aam dec 699 if vyo != -187
ey dec -2 if hr == 1668
wez dec -879 if hnh == -1994
bz dec -475 if kp < 1104
n inc -348 if fu > 635
px inc 779 if wez != 952
px inc -194 if gx < -22
ey dec -11 if x != 237
hr dec -79 if aam != 31
q inc -223 if wez < 954
hr dec 965 if kp == 1099
x dec 148 if lk < 1535
kp dec 166 if d < -2302
hnh dec -780 if oiy >= 715
phf inc 388 if vyo != -182
px dec -964 if hk != 2381
hri inc -244 if ey != -1294
rjt inc 161 if ey == -1287
x inc 871 if phf > -1924
bz dec 659 if d == -2298
fu dec -528 if ey == -1287
hnh inc 49 if px > -1003
bz dec 111 if gx != -26
d inc -268 if hr == 788
vyo inc -255 if gx == -17
pce inc -574 if pce <= -5465
gx inc -519 if yl < -706
hnh inc -115 if ey <= -1286
hr dec -633 if aam != 37
hk dec 397 if pce < -6032
hk inc -938 if lk < 1524
aam inc 893 if gx <= -535
sy inc 48 if zbh <= -24
bz dec -408 if d <= -2561
px inc 169 if yl != -701
aam dec 722 if vyo == -442
pce inc -157 if rjt == -797
vyo dec 131 if lk >= 1525
yl dec -88 if px > -834
hr dec 909 if d <= -2561
d inc 114 if d != -2568
zbh dec 780 if kp > 1095
kp dec 403 if n < -1108
fu dec -604 if rjt > -807
hr inc 142 if bz == 212
sy inc 644 if hri >= -1761
aam dec 40 if q != -3378
aam inc -333 if vyo <= -568
lk inc -849 if fu < 1782
phf dec 699 if phf == -1918
d inc 389 if fu <= 1779
px inc -231 if rjt != -801
yl dec -305 if yl == -620
x dec 303 if bz > 207
lk inc -400 if kp <= 700
wez dec -285 if rjt != -798
kp dec -254 if n > -1119
rjt dec 464 if hk != 1993
x dec -274 if oiy != 715
zbh dec -480 if hr < 664
x inc -451 if sy < -1227
phf dec -698 if kp <= 959
yl inc -665 if yl >= -324
oiy inc 816 if q >= -3377
q dec -863 if yl <= -979
sy inc 832 if x == 490
x inc -651 if phf > -1915
phf dec 51 if wez > 1228
n inc -102 if oiy <= 1536
x dec -434 if px <= -1058
ey dec -198 if fu == 1773
d inc 669 if eai >= 3973
hk inc 796 if phf <= -1961
phf dec 265 if n > -1225
vyo inc 705 if pce == -6186
vyo dec 804 if gx >= -541
lk inc 615 if gx <= -528
vyo inc -86 if lk > 882
d dec 844 if phf == -2235
lk dec -368 if phf != -2226
pce dec 965 if rjt > -1267
aam dec -751 if hnh != -1280
q inc 145 if px <= -1058
yl dec -995 if hk > 2786
hri inc 146 if wez >= 1235
hk inc 808 if hri != -1626
zbh dec -185 if oiy > 1528
hr dec -741 if zbh != -145
vyo inc -952 if oiy < 1542
bz dec 237 if hr < 1404
pce dec 835 if n == -1217
pce inc 428 if lk == 1263
gx dec -27 if hr <= 1403
wez dec -561 if aam == -174
bz dec -112 if hri < -1611
lk dec 215 if sy > -1233
x inc -919 if kp <= 954
bz dec -122 if n > -1224
q inc -203 if fu >= 1786
sy dec -518 if d <= -2902
fu inc 885 if rjt <= -1257
hk inc -20 if wez < 1800
hr inc -265 if vyo == -2408
oiy dec -930 if lk > 1041
bz dec 871 if wez > 1798
hri inc 640 if hri == -1625
phf dec 759 if hnh < -1270
hk dec -576 if x == 2
gx dec -730 if rjt != -1259
aam dec -727 if gx != 222
vyo dec -788 if eai == 3973
zbh inc 414 if hnh < -1288
sy dec 227 if wez < 1800
x inc 937 if pce <= -8001
x dec -273 if eai != 3963
d dec 995 if n == -1217
hr inc 315 if zbh == -140
d inc -277 if eai > 3965
eai inc 125 if hnh > -1284
wez inc 729 if zbh <= -137
px inc -257 if yl <= -974
n inc -634 if pce > -8006
x inc -659 if bz >= 205
phf inc -852 if fu > 2666
pce dec -791 if sy <= -940
sy inc -896 if pce == -7207
vyo dec -534 if yl < -989
rjt dec -938 if ey >= -1277
pce inc -864 if vyo > -2414
hr dec -949 if ey >= -1291
hnh inc 132 if bz < 210
gx inc -770 if sy > -948
hk inc 361 if d > -4186
bz dec -215 if px != -1319
hr dec 189 if q < -2359
ey dec 166 if hk == 3929
pce inc -278 if px == -1319
yl dec 713 if kp > 946
gx inc -328 if n < -1855
fu inc 47 if px > -1315
bz dec 47 if lk != 1038
hr inc -633 if zbh < -134
q dec 763 if hri > -1624
aam dec 12 if ey != -1448
hnh dec 617 if hnh < -1142
hnh inc -497 if eai > 4094
lk inc 443 if phf < -2984
bz inc -927 if hnh <= -2270
ey inc 272 if aam != 542
hr inc 595 if zbh == -144
hnh dec 156 if eai <= 4093
lk inc -672 if phf <= -2986
pce inc 641 if pce > -7483
aam dec -63 if x == -383
gx inc 723 if eai <= 4096
bz dec 942 if pce <= -7483
oiy inc 511 if ey != -1186
d dec -685 if lk > 824
oiy inc 360 if bz == -780
oiy inc 368 if eai < 4100
q dec -889 if d < -4178
sy dec 193 if sy <= -939
hr inc 714 if kp <= 950
pce dec -68 if zbh <= -135
lk inc 850 if x >= -383
d inc -409 if zbh >= -145
lk inc -849 if x > -379
bz dec 734 if fu < 2669
hri inc 604 if hr <= 2560
yl inc -426 if hr <= 2559
fu inc -621 if bz != -1510
hr inc 175 if q >= -2246
d dec -426 if gx < 181
phf dec -650 if aam != 604
wez dec 382 if fu == 2041
px dec 177 if hnh >= -2258
oiy dec 177 if fu >= 2032
rjt dec -937 if hk >= 3926
yl dec 885 if hnh >= -2270
px inc -752 if ey < -1175
wez inc -555 if ey >= -1183
eai inc 493 if n <= -1846
zbh inc 508 if lk >= 1657
ey inc 378 if rjt > -326
hri inc 575 if eai >= 4581
hnh inc -509 if aam != 594
oiy dec 798 if bz == -1514
hri inc 463 if hk != 3929
oiy inc -854 if vyo != -2415
ey inc -444 if q <= -2245
x dec 520 if aam <= 606
phf dec -988 if zbh < 360
vyo dec -27 if bz == -1519
oiy inc -139 if vyo != -2408
lk inc 522 if sy <= -1132
phf dec -365 if bz <= -1512
q inc -518 if pce >= -7418
hnh dec 680 if n < -1844
gx inc -198 if aam > 609
hri dec -554 if wez > 1583
d inc -925 if eai >= 4587
fu inc 709 if oiy > 2589
sy inc -138 if eai > 4581
oiy dec 538 if hnh <= -3446
phf dec -664 if gx > 171
x inc 609 if d >= -5091
hr inc -320 if vyo <= -2419
zbh inc 682 if hr == 2726
oiy dec -357 if eai > 4584
zbh inc -378 if px <= -2072
px inc 860 if ey < -797
x inc -578 if n > -1852
d dec 953 if kp >= 946
q dec 992 if gx == 174
ey dec 244 if hr != 2721
ey inc 502 if pce < -7405
hri dec -638 if sy > -1277
d dec -848 if d != -6049
kp inc -977 if eai > 4581
wez dec -114 if hk != 3929
n inc -119 if vyo > -2409
d dec -367 if ey == -545
n dec 853 if phf <= -1957
ey inc 191 if kp > -32
sy dec 909 if pce > -7416
rjt inc 255 if zbh > 1046
n inc -400 if wez <= 1592
d inc -90 if x < -864
hri inc -500 if gx != 170
hr inc 212 if vyo < -2407
d inc 309 if px != -1213
aam inc -743 if n >= -3113
hr dec -969 if phf != -1960
sy dec -612 if d >= -4613
n inc 397 if aam >= -144
aam dec 913 if d != -4602
ey inc -338 if q <= -3751
hnh inc 633 if lk > 2181
hnh inc 390 if d >= -4601
kp inc 563 if x < -862
hr dec -368 if phf < -1966
yl inc 670 if wez > 1584
x dec -486 if lk == 2188
fu dec 234 if wez == 1590
q dec 999 if d != -4611
yl dec 0 if d != -4610
rjt dec 226 if eai >= 4589
px inc 210 if kp == 531
ey inc 100 if vyo > -2420
aam inc -487 if bz < -1513
gx inc -932 if hri == 255
pce dec 524 if d >= -4602
gx inc 474 if ey == -592
x inc -329 if yl < -2340
lk inc -112 if d <= -4606
phf dec -302 if q < -4747
sy dec 8 if hnh >= -2825
bz dec 261 if fu < 1814
hk inc 775 if kp < 545
oiy dec 425 if q > -4755
eai dec 646 if zbh >= 1048
hnh inc 298 if sy < -1579
px dec 651 if phf <= -1659
sy dec 490 if zbh < 1055
yl dec -795 if hnh >= -2824
rjt dec 265 if oiy <= 1981
hk inc 766 if zbh < 1057
x dec 910 if lk <= 2084
zbh inc -186 if phf < -1654
phf dec -864 if yl > -1545
hnh inc 787 if zbh < 869
q inc -860 if wez >= 1597
fu inc -654 if x >= -1290
lk dec 865 if n > -2711
hnh inc -507 if oiy == 1976
d dec -875 if wez != 1599
sy dec 459 if n <= -2702
vyo inc 229 if lk <= 1212
bz inc -823 if eai < 3942
hnh inc 184 if rjt >= -302
sy dec -479 if sy == -2533
kp inc -376 if hr == 3907
kp inc -734 if rjt <= -289
kp inc -410 if yl >= -1544
n dec 428 if lk > 1209
d dec -825 if ey == -592
eai inc 922 if gx > -294
bz inc -174 if rjt <= -292
x dec -850 if hr > 3902
gx dec -343 if zbh >= 863
ey dec -326 if hri == 264
aam dec 869 if wez > 1588
wez inc -647 if fu != 1814
n inc -418 if x <= -445
wez dec -877 if q > -4753
ey inc 694 if ey != -593
zbh dec -178 if rjt <= -291
bz dec 465 if pce == -7415
px dec 22 if aam < -2407
hk inc -11 if oiy > 1975
aam dec -405 if yl != -1538
n inc -546 if rjt == -295
n inc 402 if hk >= 5466
kp dec 780 if phf == -799
aam dec 731 if px > -1885
zbh dec 254 if gx > 56
hnh dec 786 if zbh <= 791
x dec -369 if zbh < 791
yl dec -620 if phf <= -797
gx inc -30 if hnh != -2629
zbh inc -978 if q != -4752
d dec -792 if fu != 1809
ey dec 210 if hri != 255
oiy inc 924 if aam <= -2725
lk inc -971 if yl != -912
yl dec -516 if hk != 5459
vyo inc -668 if wez == 1820
n inc 79 if kp > -1766
gx dec 301 if ey < 108
bz dec -839 if x > -79
x dec -394 if gx == -272
oiy dec 281 if hri <= 255
zbh dec 850 if n <= -4013
phf inc -318 if ey >= 111
fu dec 447 if oiy > 2623
vyo inc 893 if kp < -1766
vyo inc -743 if n != -4020
bz dec 685 if kp == -1764
bz inc -18 if sy > -2533
wez dec 460 if yl <= -919
hr dec 157 if wez >= 1353
yl inc -440 if vyo < -2856
wez inc -795 if n == -4020
d inc -99 if sy >= -2533
rjt inc 252 if yl != -916
kp inc 295 if oiy >= 2625
hri dec 110 if hri >= 253
bz inc -956 if pce <= -7410
lk inc 657 if q == -4752
yl inc 871 if zbh != -54
lk inc 255 if px == -1893
eai dec -192 if eai <= 4871
d dec 974 if ey > 97
aam dec 556 if wez != 566
oiy dec -246 if sy <= -2523
aam dec -697 if d <= -3186
hri dec -549 if hnh == -2633
x inc -890 if hnh != -2633
phf dec -525 if hr < 3749
hri dec -581 if n > -4024
eai dec 824 if hr <= 3757
d dec 346 if rjt > -45
kp inc 145 if q > -4757
oiy inc 292 if rjt != -53
hr dec 909 if px > -1886
bz dec 934 if d > -3541
hk dec -502 if hnh != -2633
aam inc -131 if wez == 575
yl dec 826 if vyo < -2852
px inc -524 if d == -3533
eai inc 465 if pce <= -7412
hr dec -100 if sy > -2530
oiy dec 565 if oiy != 3158
bz dec 555 if q > -4759
fu dec -269 if gx == -272
wez inc 824 if eai == 4698
d dec 179 if zbh != -60
bz dec 815 if aam < -2589
hnh inc 842 if pce > -7409
eai dec -547 if hri == 1275
sy dec 984 if zbh > -65
px inc -674 if yl == -874
n inc -537 if sy >= -3510
fu dec -820 if vyo > -2855
gx inc -396 if wez <= 1395
n dec 106 if wez < 1390
ey dec 634 if rjt > -51
hk dec -888 if eai < 5248
oiy inc -48 if gx == -668
aam dec -631 if eai < 5248
fu dec -966 if n != -4656
eai dec 973 if wez < 1393
hnh dec -183 if rjt >= -39
q dec -210 if ey >= -541
hri dec 810 if ey != -526
vyo dec 971 if lk != 906
bz inc -61 if hr < 2943
wez dec -640 if px == -3082
pce inc 971 if gx > -674
yl inc 795 if eai >= 4265
kp inc 667 if bz <= -5597
bz dec 737 if fu != 3424
q inc -607 if x == 318
px inc -559 if gx >= -669
d dec 756 if wez >= 2031
gx inc -361 if aam > -1969
d inc -861 if hri <= 468
q inc 370 if pce >= -6444
fu inc -123 if hr < 2935
px dec 69 if gx >= -1026
hr dec -684 if n >= -4655
wez dec -329 if pce == -6444
kp dec 495 if phf > -807
q inc -107 if ey > -542
ey dec 112 if sy != -3509
wez inc 877 if ey == -532
fu dec -538 if lk >= 888
hk inc -430 if gx != -1036
q inc 683 if hk < 5922
hri inc -113 if x <= 314
lk inc -236 if vyo < -3825
sy dec -981 if hr <= 2948
hr dec 190 if sy <= -2533
px dec -827 if aam < -1958
hri dec -954 if hk == 5917
aam inc 517 if px < -2810
phf inc 857 if zbh > -63
hri dec 111 if fu < 3955
phf dec 59 if n != -4671
eai inc -609 if lk == 897
eai dec 718 if sy > -2522
bz inc 776 if ey >= -535
kp inc 482 if kp == -1152
q dec 802 if yl == -79
wez inc -486 if hr >= 2940
pce dec 876 if d <= -4574
px dec -407 if yl >= -84
vyo dec -512 if vyo < -3819
phf dec 868 if eai != 3659
oiy inc 785 if hnh <= -2633
sy dec 111 if phf == -869
wez dec -895 if n == -4663
hri dec 903 if yl == -73
pce dec 871 if pce == -6444
yl inc -555 if rjt >= -45
kp inc -488 if phf > -868
phf inc 819 if phf == -879
d dec -490 if d == -4573
oiy dec -578 if fu <= 3958
x inc 536 if aam >= -1453
x inc 212 if yl > -637
fu inc 273 if hnh <= -2639
hk inc -539 if fu == 3953
oiy dec -271 if sy <= -2636
kp dec 315 if ey == -532
wez inc 96 if hr >= 2948
n inc 613 if d <= -4074
zbh inc 704 if hr <= 2946
bz inc 405 if pce > -7325
px inc -97 if rjt >= -48
q inc 924 if n > -4046
aam dec -101 if d == -4088
hnh dec -964 if kp == -985
hk inc -489 if bz >= -5160
hri inc -810 if vyo == -3313
zbh inc -76 if fu <= 3962
pce inc 563 if aam < -1436
bz inc -662 if pce > -6761
rjt dec 917 if ey < -536
zbh dec -399 if pce <= -6759
wez dec -319 if yl < -641
vyo dec 872 if fu == 3946
hri dec -424 if hri != 498
gx inc -735 if x != 1058
hk inc -795 if ey != -540
aam dec 185 if hnh < -1664
q dec 512 if n > -4056
n dec 988 if lk != 907
sy inc -961 if ey != -526
px dec 8 if gx != -1766
oiy dec -533 if wez < 3651
eai dec 526 if oiy <= 4718
d inc -741 if rjt == -43
sy dec -666 if oiy < 4718
hri dec 503 if n != -5033
pce inc -209 if lk >= 896
eai dec -81 if hnh != -1667
gx inc 313 if eai != 3219
hri dec 540 if zbh <= 574
pce dec -371 if hri == -545
hri dec -560 if aam > -1635
kp inc 777 if n == -5038
pce inc 206 if kp >= -216
zbh dec 507 if d < -4819
d dec -186 if n < -5034
aam inc 573 if hr <= 2943
wez dec -465 if phf == -867
rjt dec -719 if wez < 3649
phf inc 450 if kp != -201
x inc -406 if hri <= 19
aam dec 782 if eai <= 3219
n dec -184 if eai > 3213
bz inc -112 if px >= -2510
phf dec 751 if vyo < -3309
wez inc 642 if aam != -1846
aam inc 543 if wez >= 4285
yl inc -97 if eai >= 3212
bz inc -310 if pce != -6391
hr dec 182 if q <= -4911
fu dec 618 if hnh == -1669
yl inc -164 if aam != -1300
yl inc -218 if px >= -2504
yl dec 718 if ey > -524
hr inc -287 if hri > 19
kp dec 767 if aam < -1294
q dec 403 if zbh <= 68
aam inc -787 if hnh >= -1673
aam inc -638 if fu != 3338
yl inc -962 if n != -4854
fu dec -34 if pce != -6377
lk dec 340 if rjt != 676
oiy dec -538 if hr != 2936
ey dec 585 if zbh >= 53
d dec -684 if vyo <= -3310
bz dec -127 if bz < -6120
eai inc 202 if vyo > -3320
wez dec 364 if fu > 3367
sy dec -101 if lk != 887
lk inc 280 if aam != -2721
n dec 677 if wez <= 3928
hnh dec 695 if kp > -983
pce inc 238 if ey > -1119
x dec -44 if x < 656
fu inc 899 if yl < -902
aam inc 5 if hnh <= -2357
pce dec 674 if ey == -1117
hk dec -918 if aam < -2712
hri inc -167 if aam != -2719
oiy dec -187 if lk == 897
hri dec 504 if hnh < -2364
hnh dec 855 if lk >= 904
rjt inc 624 if oiy <= 5442
pce inc -704 if phf >= -1163
eai dec -156 if yl > -903
kp dec 719 if oiy != 5446
x dec 675 if bz <= -6010
yl inc -266 if kp >= -1702
aam dec 955 if eai != 3583
ey dec -312 if oiy < 5451
px inc 162 if rjt != 1296
phf dec 559 if wez < 3930
x inc -773 if zbh != 66
pce inc 14 if px < -2347
zbh inc -527 if gx <= -1458
hk inc 76 if rjt != 1296
hri inc -79 if q != -5323
wez inc -462 if d < -3956
rjt dec -394 if phf != -1726
phf inc -728 if sy == -2833
x inc -747 if gx <= -1447
hri inc 308 if gx != -1461
eai dec -921 if gx < -1447
hnh inc 573 if oiy > 5447
q dec -56 if q == -5313
zbh inc 46 if vyo == -3313
fu inc -651 if hnh != -2364
hri dec -201 if fu > 3372
vyo dec -953 if vyo >= -3319
wez dec 461 if x < -860
px dec -31 if n < -5528
pce inc -637 if eai >= 4499
rjt inc 898 if kp > -1697
n dec -290 if x < -852
oiy dec 72 if zbh == 105
fu inc 300 if hnh <= -2363
fu dec 587 if yl > -1167
sy inc 97 if lk == 897
oiy dec -593 if rjt >= 2585
fu dec 386 if hnh < -2354
px inc -618 if px > -2320
pce inc -205 if rjt == 2592
lk dec -117 if fu <= 2697
aam dec 463 if vyo > -2364
ey dec -992 if zbh < 113
phf inc 219 if ey == 187
gx dec -365 if yl < -1159
q dec -80 if kp >= -1700
bz dec 624 if ey == 187
vyo inc -705 if aam == -4134
phf inc 919 if zbh <= 113
phf inc 215 if lk <= 1018
hk inc -670 if rjt < 2586
hri dec -31 if n > -5240
hk dec -575 if hr != 2940
px inc 475 if lk == 1014
kp dec -805 if aam == -4134
gx dec 804 if aam != -4134
wez dec -249 if aam == -4134
wez inc -637 if q == -5183
hnh dec -91 if hr < 2948
rjt inc -906 if n <= -5234
aam dec 535 if phf != -1094
wez dec -620 if lk < 1018
fu dec -585 if n < -5236
kp dec -253 if bz < -6625
fu dec 395 if rjt >= 1685
d inc -187 if hk != 5653
q inc -432 if px == -2462
sy inc -185 if ey == 187
kp dec -487 if x <= -859
gx dec -76 if x <= -859
pce dec 327 if sy <= -2925
gx inc -356 if px >= -2471
q dec 191 if zbh >= 98
gx inc -115 if pce != -7005
phf inc 349 if aam <= -4667
yl inc 434 if rjt < 1683
eai dec -6 if hri >= 79
kp inc -392 if aam < -4664
rjt dec -776 if rjt != 1680
vyo inc 972 if hnh >= -2279
x inc 126 if oiy >= 5962
zbh inc 185 if x != -731
x inc -113 if fu == 2886
hr inc -637 if ey != 181
d inc -887 if wez <= 4333
oiy dec 92 if rjt == 2462
sy dec -179 if n >= -5241
phf dec -836 if hnh != -2279
rjt inc 472 if phf == 81
rjt dec -439 if rjt > 2930
px inc 774 if q == -5800
rjt dec -154 if kp <= -792
hnh inc 706 if px > -1686
pce dec -655 if hri == 77
sy inc 483 if oiy < 5874
aam inc 686 if hri <= 82
fu dec -35 if oiy < 5880
phf dec 462 if vyo == -2093
rjt dec -635 if pce <= -6351
fu inc 963 if lk >= 1005
d dec 853 if n > -5244
eai inc 682 if sy <= -2254
pce dec -900 if lk > 1013
x dec 787 if d <= -5876
lk inc -981 if eai == 5179
aam inc -878 if gx >= -1472
ey inc 310 if vyo > -2100
kp inc -428 if oiy == 5871
kp dec -564 if kp != -1221
gx inc -889 if bz < -6615
vyo inc 631 if pce < -5448
zbh dec -57 if oiy > 5867
fu dec 987 if aam > -3991
hk inc 522 if fu != 2897
wez dec -488 if eai >= 5177
lk dec 466 if bz > -6626
kp inc 848 if wez < 4821
fu dec -123 if pce <= -5454
ey dec -699 if bz >= -6622
fu inc -452 if n > -5248
x dec 455 if wez > 4808
px inc 832 if eai == 5179
rjt inc 831 if x != -2090
bz inc -307 if gx <= -2365
d dec 637 if kp <= 199
yl dec 14 if bz < -6929
fu inc 249 if yl < -1166
pce dec 853 if px <= -854
hri dec 992 if phf != -381
sy inc -43 if yl <= -1166
fu inc 852 if bz != -6924
q inc -706 if fu != 3669
vyo inc -755 if x < -2080
vyo dec 183 if n >= -5235
ey dec -500 if q > -5809
n inc 148 if ey == 997
x inc -299 if zbh == 347
pce inc 409 if sy <= -2307
ey inc -72 if hri < 81
q dec -413 if wez >= 4810
kp dec 648 if q == -5382
wez inc 18 if pce < -6315
fu dec -587 if px > -859
oiy inc -107 if x < -2393
zbh dec 724 if n > -5102
wez dec 233 if phf > -390
zbh inc 578 if px == -862
hk dec 454 if hr < 2312
kp inc -473 if ey >= 923
x inc -166 if zbh == -377
q dec 540 if fu >= 4252
hnh inc 952 if eai < 5187
sy inc -385 if sy >= -2305
hr inc -207 if q == -5927
zbh inc -577 if hri == 77
eai dec 752 if gx >= -2379
px dec -309 if hnh != -1319
hr dec 635 if x == -2555
d inc -319 if phf > -383
n dec 415 if rjt <= 4170
gx dec -420 if n <= -5500
gx dec 740 if oiy != 5863
phf dec -251 if d < -6834
gx inc -494 if hnh < -1319
aam dec 10 if aam >= -3974
eai inc 449 if sy == -2693
rjt dec 60 if px == -547
bz dec -492 if yl <= -1169
yl dec -616 if hk != 5219
q dec -613 if hk != 5209
eai dec 273 if bz >= -6440
rjt inc -508 if ey == 925
n inc 238 if bz == -6439
oiy dec -95 if aam >= -3987
sy dec 814 if gx != -3184
gx dec -55 if vyo >= -2226
phf inc 851 if eai > 4157
hri dec -795 if d < -6830
eai inc 302 if pce >= -6317
eai inc -325 if hr < 1470
n dec 27 if hri >= 866
gx inc 5 if ey < 916
rjt inc -252 if pce >= -6302
hnh dec -894 if ey > 920
aam dec 380 if x > -2551
kp dec 181 if px >= -545
eai dec -933 if bz == -6439
hk inc -307 if zbh <= -961
lk dec -218 if ey == 925
bz inc 948 if yl <= -554
pce dec 497 if sy < -2685
wez inc 46 if x <= -2560
hr dec 160 if hnh != -432
hri inc -747 if px < -543
zbh dec -437 if sy >= -2693
hk inc 500 if n > -5293
yl dec -116 if wez >= 4583
wez dec -790 if hri == 125
vyo inc 969 if yl > -448
hk inc 353 if sy < -2677
aam dec -106 if hnh < -432
oiy inc -511 if sy != -2687
aam inc 420 if lk > -225
rjt inc -50 if zbh == -517
yl dec 818 if hr < 1308
rjt dec -631 if gx > -3132
vyo inc -240 if yl < -1253
bz dec -391 if vyo != -1496
hri dec 159 if yl == -1261
hri dec -276 if kp <= -277
ey dec 885 if d == -6837
q dec 135 if d <= -6840
fu inc -810 if ey > 40
hri dec -170 if px <= -544
ey dec 763 if hk <= 5560
lk inc -576 if zbh != -514
x dec 415 if px <= -544
hr dec -527 if sy <= -2684
fu dec -998 if vyo > -1492
q inc 484 if d > -6845
phf dec -29 if vyo == -1488
gx inc -660 if bz >= -5097
sy dec 878 if oiy < 5975
x dec 365 if oiy < 5971
fu inc 131 if rjt == 4175";

        private const string Example = @"ab inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

    }
}