using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day05 : IDay
    {
        public int Day => 5;
        public int Year => 2021;

        public void ProblemOne()
        {
            var input = ParseInput(Input).Where(i => i.start.X == i.end.X || i.start.Y == i.end.Y).ToList();
            var result = CountIntersections(input);
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
            var input = ParseInput(Input);
            var result = CountIntersections(input);
            Console.WriteLine(result);
        }

        private int CountIntersections(List<(Vector2i start, Vector2i end)> lines)
        {
            var points = new Dictionary<Vector2i, int>();
            foreach (var line in lines)
            {
                var actualLine = line.start.PlotLine(line.end);
                foreach (var p in actualLine)
                {
                    if (points.ContainsKey(p))
                    {
                        points[p]++;
                    }
                    else
                    {
                        points[p] = 1;
                    }
                }
            }
            return points.Count(i => i.Value > 1);
        }


        private List<(Vector2i start, Vector2i end)> ParseInput(string input)
        {
            var result = new List<(Vector2i start, Vector2i end)>();
            var lines = input.SplitNewLine();
            foreach (var l in lines)
            {
                var parts = l.Split("->");
                var start = new Vector2i(int.Parse(parts[0].Split(',')[0]), int.Parse(parts[0].Split(',')[1]));
                var end = new Vector2i(int.Parse(parts[1].Split(',')[0]), int.Parse(parts[1].Split(',')[1]));
                result.Add((start, end));
            }
            return result;
        }


        private const string Example = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

        private const string Input = @"629,581 -> 123,75
921,643 -> 452,643
498,588 -> 503,593
861,137 -> 102,896
603,339 -> 603,137
138,738 -> 117,738
14,60 -> 41,60
606,810 -> 157,361
980,21 -> 56,945
43,731 -> 910,731
745,329 -> 962,329
800,916 -> 390,916
737,96 -> 737,24
978,777 -> 978,592
232,638 -> 232,611
833,888 -> 454,509
627,659 -> 763,523
871,300 -> 310,861
987,224 -> 987,909
54,234 -> 852,234
413,111 -> 277,111
264,600 -> 840,24
180,477 -> 780,477
837,197 -> 837,796
943,438 -> 769,438
260,801 -> 318,801
645,717 -> 593,717
542,677 -> 115,250
255,251 -> 726,722
57,219 -> 57,147
898,683 -> 466,251
925,900 -> 697,900
264,384 -> 12,384
240,584 -> 816,584
962,932 -> 151,121
524,163 -> 253,434
981,557 -> 981,942
934,176 -> 454,656
872,439 -> 111,439
449,57 -> 161,57
50,72 -> 50,54
104,141 -> 603,141
219,886 -> 747,358
774,257 -> 110,921
82,142 -> 845,905
416,859 -> 129,572
326,640 -> 181,785
192,818 -> 192,408
309,876 -> 309,811
536,860 -> 536,740
789,472 -> 789,625
760,135 -> 647,22
425,788 -> 329,884
13,11 -> 971,969
342,772 -> 456,772
85,758 -> 500,343
322,167 -> 830,675
977,117 -> 107,117
148,902 -> 134,902
812,940 -> 45,173
544,218 -> 88,674
110,536 -> 110,927
989,127 -> 129,987
89,96 -> 671,678
604,368 -> 604,127
89,551 -> 89,460
590,749 -> 590,147
390,224 -> 899,224
25,765 -> 488,302
624,265 -> 339,265
127,712 -> 127,797
133,53 -> 133,168
934,978 -> 638,978
443,119 -> 672,119
691,796 -> 486,591
153,64 -> 153,859
588,78 -> 381,78
205,655 -> 90,655
965,625 -> 965,388
699,500 -> 699,227
35,246 -> 791,246
305,372 -> 305,326
954,695 -> 416,695
192,582 -> 712,62
759,87 -> 346,500
73,153 -> 903,983
386,12 -> 937,12
287,256 -> 19,524
725,761 -> 391,427
159,128 -> 159,985
839,853 -> 55,69
818,257 -> 974,257
754,645 -> 738,645
164,950 -> 904,210
208,370 -> 381,370
467,876 -> 42,876
779,708 -> 779,56
152,504 -> 465,817
808,721 -> 965,564
957,131 -> 649,131
984,12 -> 23,973
283,915 -> 283,347
775,13 -> 340,448
588,294 -> 588,360
775,976 -> 775,497
891,292 -> 551,292
43,860 -> 849,860
639,384 -> 639,942
932,967 -> 932,762
109,66 -> 828,785
107,369 -> 107,480
606,445 -> 766,605
429,10 -> 588,10
895,832 -> 586,523
938,633 -> 938,152
907,683 -> 242,683
748,384 -> 748,771
256,276 -> 954,276
975,444 -> 975,33
404,469 -> 84,469
105,688 -> 55,688
73,105 -> 695,105
402,335 -> 402,567
524,797 -> 524,603
188,171 -> 61,44
954,30 -> 11,973
794,400 -> 510,116
592,845 -> 375,845
457,679 -> 634,679
35,635 -> 641,635
652,667 -> 541,556
393,128 -> 393,884
302,254 -> 302,297
302,145 -> 279,168
64,274 -> 432,274
560,154 -> 560,511
928,755 -> 928,722
578,430 -> 891,430
505,463 -> 505,476
62,248 -> 661,248
573,603 -> 573,781
61,800 -> 61,723
925,357 -> 925,233
883,336 -> 753,466
535,647 -> 48,160
981,931 -> 269,219
980,981 -> 14,15
404,675 -> 112,383
861,472 -> 568,765
17,439 -> 17,530
839,411 -> 754,411
944,408 -> 793,257
910,963 -> 910,382
640,101 -> 987,101
33,100 -> 779,846
799,981 -> 799,985
787,610 -> 787,990
967,567 -> 502,567
369,452 -> 876,959
830,725 -> 604,499
112,255 -> 726,869
746,291 -> 930,475
170,795 -> 170,72
587,183 -> 981,183
588,226 -> 588,328
643,747 -> 504,747
882,445 -> 627,445
849,274 -> 849,135
536,225 -> 212,225
143,538 -> 143,832
319,25 -> 984,690
278,189 -> 278,526
527,414 -> 527,704
935,141 -> 122,954
623,626 -> 111,114
211,495 -> 211,924
146,914 -> 836,224
573,423 -> 956,423
902,188 -> 463,188
807,950 -> 925,950
956,60 -> 35,981
791,480 -> 383,888
886,872 -> 886,471
441,840 -> 65,464
367,596 -> 367,846
566,799 -> 574,799
590,202 -> 803,202
988,17 -> 17,988
566,640 -> 63,137
304,316 -> 304,470
452,808 -> 452,455
982,647 -> 494,159
654,102 -> 654,580
760,122 -> 610,272
349,859 -> 114,624
72,520 -> 72,790
272,910 -> 272,848
751,311 -> 751,911
396,771 -> 396,356
37,909 -> 904,42
903,636 -> 939,636
661,911 -> 661,967
246,367 -> 246,451
179,659 -> 455,935
65,977 -> 975,67
525,539 -> 525,523
211,310 -> 850,310
327,158 -> 961,158
224,46 -> 15,255
177,624 -> 177,297
949,833 -> 949,711
732,43 -> 616,159
537,397 -> 112,822
432,490 -> 509,567
70,130 -> 872,932
810,584 -> 810,679
863,967 -> 145,249
919,840 -> 574,840
955,534 -> 955,77
90,685 -> 90,858
24,974 -> 986,12
980,940 -> 115,75
41,154 -> 705,818
196,976 -> 901,271
80,855 -> 526,409
190,314 -> 818,942
195,400 -> 195,968
698,976 -> 698,171
351,753 -> 292,753
433,163 -> 433,411
37,615 -> 62,615
696,724 -> 696,170
625,793 -> 625,359
387,469 -> 387,552
24,568 -> 522,70
569,695 -> 272,695
16,87 -> 634,705
986,611 -> 986,827
581,196 -> 581,180
373,716 -> 373,304
562,767 -> 562,493
506,430 -> 474,430
362,878 -> 624,616
888,288 -> 33,288
483,480 -> 709,706
261,879 -> 896,879
196,71 -> 196,462
717,414 -> 296,414
973,591 -> 973,149
390,140 -> 390,727
966,932 -> 913,932
693,824 -> 902,824
724,898 -> 724,46
557,802 -> 902,802
968,398 -> 968,124
784,727 -> 498,441
938,618 -> 938,863
119,114 -> 119,636
110,933 -> 374,933
406,760 -> 895,271
499,526 -> 834,526
844,464 -> 844,535
32,899 -> 903,28
796,423 -> 796,498
188,144 -> 965,144
135,828 -> 591,372
616,558 -> 616,129
356,818 -> 356,540
406,894 -> 519,894
303,31 -> 821,549
82,472 -> 708,472
64,314 -> 355,314
236,341 -> 489,341
839,118 -> 544,118
680,804 -> 96,220
204,105 -> 906,807
357,662 -> 685,334
463,797 -> 555,797
973,913 -> 276,216
614,852 -> 25,263
958,275 -> 812,421
963,15 -> 26,952
743,136 -> 328,136
975,937 -> 625,937
984,34 -> 38,980
19,516 -> 432,103
802,827 -> 802,78
12,971 -> 945,38
335,331 -> 290,331
890,803 -> 170,803
950,52 -> 950,417
68,391 -> 524,847
862,699 -> 786,699
542,323 -> 578,323
454,171 -> 970,687
980,24 -> 990,24
253,56 -> 600,403
571,27 -> 622,27
966,400 -> 527,400
624,914 -> 624,43
85,819 -> 764,140
204,76 -> 958,830
208,77 -> 208,284
668,342 -> 668,373
633,468 -> 786,621
972,704 -> 980,704
552,601 -> 552,953
912,28 -> 199,741
884,403 -> 491,10
731,897 -> 115,281
492,33 -> 492,296
295,130 -> 691,130
741,389 -> 403,51
974,64 -> 68,970
954,518 -> 629,518
392,722 -> 242,872
523,762 -> 183,422
431,664 -> 782,313
750,696 -> 665,696
426,243 -> 308,243
602,857 -> 602,598
849,682 -> 599,682
723,514 -> 447,514
403,898 -> 598,898
139,555 -> 124,555
570,151 -> 135,151
205,99 -> 119,185
291,271 -> 647,627
537,541 -> 871,207
647,596 -> 630,596
870,967 -> 51,148
470,205 -> 470,692
238,914 -> 238,55
285,661 -> 578,661
878,343 -> 140,343
274,175 -> 274,944
193,829 -> 193,332
169,255 -> 824,910
695,389 -> 472,389
707,336 -> 543,336
860,983 -> 567,690
732,595 -> 42,595
723,603 -> 161,603
206,937 -> 328,937
981,26 -> 62,26
624,696 -> 624,756
317,626 -> 317,717
353,475 -> 353,809
759,54 -> 303,54
96,493 -> 70,493
457,675 -> 457,812
955,577 -> 955,673
10,606 -> 559,606
945,872 -> 555,872
818,651 -> 818,51
869,314 -> 90,314
271,490 -> 458,490
48,880 -> 48,495
310,62 -> 310,694
61,988 -> 985,64
558,128 -> 745,315
594,695 -> 549,695
98,114 -> 98,204
107,513 -> 577,983
721,859 -> 150,288
102,101 -> 903,902
971,547 -> 501,547
857,127 -> 290,694
486,117 -> 230,117
550,46 -> 968,464
946,965 -> 40,59
757,565 -> 757,613
99,597 -> 99,763
352,287 -> 352,22
826,781 -> 942,781
631,667 -> 631,869
438,778 -> 736,480
974,988 -> 12,26
730,69 -> 417,382
879,987 -> 10,118
433,256 -> 142,256
254,285 -> 941,972
828,351 -> 257,922
830,751 -> 830,347
789,244 -> 355,244
607,451 -> 607,838
853,198 -> 265,198
65,738 -> 65,921
122,676 -> 122,801
493,252 -> 639,252
42,977 -> 937,82
544,296 -> 271,23
772,436 -> 772,979
259,403 -> 259,757
436,193 -> 436,478
227,395 -> 216,395
672,205 -> 711,244
116,307 -> 116,337
768,332 -> 768,314
380,867 -> 380,746
57,357 -> 57,247
326,502 -> 640,188
151,512 -> 308,512
481,226 -> 481,935
835,205 -> 868,238
535,920 -> 535,158
314,106 -> 221,13
304,189 -> 357,189
349,169 -> 349,150
568,765 -> 849,484
680,877 -> 392,589
170,924 -> 984,924
52,935 -> 714,273
542,667 -> 708,667
583,522 -> 263,842
710,50 -> 710,500
713,272 -> 713,897
70,843 -> 70,747
319,874 -> 290,874
56,148 -> 115,89
77,136 -> 928,987
867,956 -> 152,241
206,171 -> 688,171
834,81 -> 834,726
186,482 -> 888,482
785,467 -> 537,467
232,100 -> 338,206
556,921 -> 556,469
630,16 -> 976,16
168,977 -> 168,383
784,819 -> 694,819
298,116 -> 47,116
577,19 -> 577,729
767,236 -> 682,236
222,277 -> 222,952
119,196 -> 18,95
26,500 -> 26,714
324,605 -> 223,706
296,224 -> 582,224
425,582 -> 425,371
922,365 -> 674,365
377,302 -> 841,766
342,99 -> 342,469
181,470 -> 181,473
201,803 -> 201,335
593,252 -> 262,583
138,14 -> 138,375
148,713 -> 148,733
208,710 -> 777,710
333,782 -> 20,782
258,680 -> 258,368
978,195 -> 301,195
600,350 -> 600,22
83,442 -> 301,442
747,173 -> 67,173
869,884 -> 869,291
832,979 -> 832,349
457,476 -> 457,472
521,372 -> 521,630
440,408 -> 830,408
530,175 -> 530,600
664,158 -> 282,158
942,757 -> 942,852
76,763 -> 76,658
379,831 -> 379,75
74,35 -> 391,35
39,349 -> 794,349
591,211 -> 226,211
143,215 -> 143,808
965,19 -> 26,958
475,33 -> 623,181
791,212 -> 791,913
95,942 -> 927,110
977,434 -> 755,656
340,762 -> 404,698
518,321 -> 61,778
238,620 -> 238,666
568,522 -> 568,757
716,821 -> 716,404
57,34 -> 929,906
949,483 -> 785,483
408,255 -> 408,191
590,62 -> 436,62
729,44 -> 591,182
557,740 -> 902,395
900,467 -> 741,467
90,258 -> 653,258
653,323 -> 420,556
85,933 -> 592,933
938,59 -> 218,779
226,467 -> 226,937
587,330 -> 587,51
487,797 -> 924,797
216,11 -> 216,875
316,263 -> 301,263
981,487 -> 981,519
97,936 -> 896,137
704,560 -> 548,560
44,340 -> 617,340
160,751 -> 787,124";
    }
}