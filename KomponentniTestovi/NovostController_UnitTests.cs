namespace KomponentniTestovi
{
    [TestFixture]
    [TestOf(typeof(NovostController))]
    internal class NovostController_UnitTests
    {
        NovostController controller;

        [OneTimeSetUp]
        public void Setup()
        {
            Slucaj[] s = { new Slucaj { ID=1509}, new Slucaj { ID = 1 }, new Slucaj { ID = 2 }, new Slucaj { ID = 3 }, new Slucaj { ID = 4 }, new Slucaj { ID = 5 } };
            controller = new NovostController(getDbContext(slucajevi:s));
        }

        [Order(1)]
        [TestCase("2023-09-15", "pic/fndkjsalfd/malopile.jpg", "imali smo pile, malo pile, jedno malo pile od tri kile", 1509)]
        [TestCase("2023-09-16", "pic/fndkjsalfd/malopile2.jpg", "ali pile nestade, idi pa ga trazi, mozda ce se javiti", 1509)]
        [TestCase("2023-09-17", "pic/fndkjsalfd/malopile3.jpg", "daj mu saku razi", 1509)]
        [TestCase("2023-09-18", "pic/fndkjsalfd/malopile4.jpg", "nasli smo pile, spalo je na dve kile", 1509)]
        [TestCase("2023-09-19", "pic/fndkjsalfd/malopile5.jpg", "opet je nestalo", 1509)]
        public async Task DodajNovost_assertOk(DateTime datum, string url,string tekst, int idSlucaja)
        {
            Novost n = new Novost() { Datum = datum, Slika=url,Tekst=tekst};
            var actionResult = await controller.DodajNovost(n, idSlucaja);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(2)]
        [TestCase("2023-09-20", "pic/fndkjsalfd/malopile.jpg", "", 1509)]
        [TestCase("2023-09-20", "pic/fndkjsalfd/malopile.jpg", "novost", 2000)]
        [TestCase("2023-09-20", "", "novost", 2000)]
        public async Task DodajNovost_assertNotOk(DateTime datum, string url, string tekst, int idSlucaja)
        {
            Novost n = new Novost() { Datum = datum, Slika = url, Tekst = tekst };
            var actionResult = await controller.DodajNovost(n, idSlucaja);
            Assert.IsNotInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        [Retry(3), Order(7)]
        public async Task ObrisiNovost_assertNotFound([Random(200, 1500, 4)] int id)
        {
            var actionResult = await controller.UkloniNovost(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [Order(3)]
        [TestCase(1, null, "2023-09-14", null)]
        public async Task IzmeniNovost_OK_assertZero(int id_novosti, string? tekst, DateTime? datum, int? id_slucaja)
        {
            Novost n = new Novost();
            n.ID = id_novosti;
            if (datum != null) n.Datum = (DateTime)datum;
            if (tekst != null) n.Tekst = tekst;
            var actionResult = await controller.IzmeniNovost(n);
            var obj = ((OkObjectResult)actionResult).Value;
            if (obj == null) Assert.Fail("Neuspesna izmena");
            Assert.Zero(((Novost)obj).Datum.CompareTo(datum));
        }

        [Order(4)]
        [Test]
        [Pairwise]
        [Retry(2)]
        public async Task IzmeniNovost_assertOk([Range(1,4,1)]int id_novosti, [Values("novi tekst")]string? tekst, [Values(null)]DateTime? datum, [Random(1,5,2)]int? id_slucaja)
        {
            Novost n = new Novost();
            n.ID = id_novosti;
            if (datum != null) n.Datum = (DateTime)datum;
            if (tekst != null) n.Tekst = tekst;
            var actionResult = await controller.IzmeniNovost(n);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [TestCase(-1,null,null,null)]
        [TestCase(1, null, "2025-03-12", null)]
        [TestCase(1, "", null, null)]
        [TestCase(1, "[2Z6y3B[,&7y?ujZa$#j-(R55B-em@vjV/+H6jY*Y%*h{4Z(/Ncn7nd*_GrNRKN?v8.2[}pNkKr;THrWK!Y0(LVr4M8Vp_Ck!0#38HzCT1Qt8&TQA?qFFVif]&_AUXiC2jh!My6Jx0$1]n#dDm_;Fmd(J[w@Eu%w+nhtqpyhVHnJ-&{3LFU&{.)zn/801xPMcP}$yCc$9GETi,_C:46=$?k.CRtH09Qce*i;Zryc)XAMj)gVr=qGZ59?VJn#hf)ckMA}Jq-8X}1Ka0)U?qS7aNH&.c.tvT#YFz_9bJ]y3Y2SD/XQ06fm!Y-&$i?XyM..t_K.$ty#BTcm+t:q,D4nM+CDgv)dH=nVUzUe$cyVv7Btj0}?;fMpRfutt(.QFX)PrXk87,BWK%2,fKYjA)k]}%[?.A6MY_1BZYP6t7A8yaVdXWy])_2{XK=/WH/?Pc#CTvMmD8E[cZEN@ZJzdmg@uRjx(j_bkz7{4@HaA;}YnVfF!EzVh3jYkBNtxabA529pN#%v.zbPbHeTdUS&,]:.:i?.e9U2--(/b-9.VVvNHP?nc(/(9LT[4tUVf;)0q6W7aXHMR*V_=/}r*w3ERjZ2Gyq]/&4mN/&GMt%a8JgtQfmX$DJw}{up!xh8J%Btv{eCNqYAq.HkK=@]c6Y*_-GCmr(nA--fHyW])K9:PH!abREu/TV5nWVk1AFr]SjawH,D1{5Gy[;_t(3/.FE#6XEHrx-J2G8]R?;)y-S*Z?-aqWdn,FG{:weLy+%v(qG3H2z4$RFN8(w3YP#XpQ?L,!MK;EJ[cN+B8tCKmr)Bv]!G;/t=0Q+9/RD.fT.V,GBvczv&?V/Zn[/i2}rq;wnVkSb}M2=xEZEU]}&R{1*g(p.tMFiZWg6(?Gvn$*i.?[D{$vg15Yp?c?KTN9FF*MMRTn&nmh0.B8Fb9#H8J$hF-JLYjVgz98D&HpX3t.jZU!:xHzj.z6@gSnP_BJ@Y29Y/:}$Q0v##{a=Rb-v&&=QhF5iq/H!3c*f?GeDq,Heg_/TfK!@0$]iY]]Kx{xFeJ?ucT[%EtjK*-zCVFm%kj?0#pJ&Lb*qAU.,?=LJ&XM18ny#Wi!Z}6SS,[3/F2_=0cqzUU%8hJD$!BZ7)t,y_R/)7x6mS@Z6%5QS&X3Qh$k:_dPP0.5fRRyD=yii0Ct%&zi{+ScRbXxn5_vTy[:z*/;yhJY(h!*p3);Py+iU8!B@t6%f!X*m/CS4UHSG4&T1[RxL1pfQRd{50(bU.XR76)1NWwYU8whw,.Hg6#B1-%uk/LN,xaf[8,Y(UCUJ-ZN5!e*2$zz[,SNZ;wmv6M+@[9(tTixX$.c25)YmyAZv_J(@g{nHF!%]Uy;Biu{G=2p[/6}xK2H!ZC5D7n_{4Y5$E{imNLTU6@KVCvTvicWj.h,J%cig+6Amd}0((Me0XVRN%KS8&{R*U$)?w,9&Vp5F.jt9qppfV9$?g7P*4$98X{trcFDpL7+p%Q/a.FRdwdRw]JQB}L$,P,$:B4Z/78JfkS+f(E+-NUqAV&b&)3CcpTxJLi[T3UA{Nh#3#y3%Wm1Lda:1[!$at=c_miU(+i-c;Ea]3c=4btt(/DUnk@#_ix[wR$dc=g+xRgka=SY?fdt7N=tHa3)wY_=t,6!RHH=z{*LyR1e*herdkxFSP-4Nj._fC3UuL@*Acphb[[*}h07c0%Wh,cq!PCJZ@B!dm(iGC-B]meA,J[kb%4fx3?Ce,T:[zZpm2&!fG-,a}JFi9QaGaxR&M7m)J6H)*&?Gf}:d.HuM)TVVQczuuRqzCGi-Rg1(VKHRRHZ!7D+QSwL]dDwnP/QdyQ-[aXP06T*H3ULTqLpcKNNLpK+M?q*]Bp5mfZh/NmJ#b?!@E&#MYUb]X4PU?_D#@+HZMx8L#Px++/0)C-Nxd1S933NZj#_}wrF$)p;3=1p,$Z.T]QwN5y(E/6atM(Pg.zPPZG)&/;0$ypqDm%2K)t({MEaG5qfG=i6[9$#JPc]euYe1*ju?%q%gp1PYQ[K25d;3%ZvJCVHLq]!Xa=#v-JUCp0jcwaGy;}G@58vf?Uib[E(X=}gNy73(,ad9f@KPt}6]&T.B5$JazQ;mH5@r:5pw8!PwJ_5hXF2T8g_np8i[t[/ZchP-2b$2)*q/yqZS!e)ygK)$jDvTyh?M_V8;RS(LazUt]m8PJ;ce&e9600He@7200a?DPJQ{RWWG41%WPiCB/FXtafL#DaPkPVnq&wj=$AJN??[$kqG%ZHX,byC[GEn?XXXV4a#$?,dA_;8vj6{mWF9QJ?58k.;-zaVHG$*Gce#;pL7ELHL.:6jC{J,}d2&b[0bigk$F.+#tbCZf@,EjXhuJX_wV4UJyhv]Z_ej[DAmH/F1]jgwR30HBUGRprfh{2(]RbEm(!{k9tAEGg_.K.5%nMW=pGGEu$?8__F{Di-qbZ)62y,E/Jqk[{FY{y:HHXRC]LwEFQpMLcp_(j3=72gW+r6#tc@gUp6}@Vrnj8ySc,JAP2%1ycj&H{8vXj,(dk763TN?.,EY[PYnX3&.kxES1:nK6zXnJ!{tdf);m+,bEk_nw*$jTagt#39#@mMU351,HF&;af*Pc+E{61keuj3YwN[97x1&JqBCA]aYELr1M4K$RauVawr@%g;C&P@PwPgW5YzK!yNpZ$SUa%/deP8&iZ7kR@W,0ZP.h(d?1q@)r#Qmmi8k!}Hb_y?=BA@k%&-thi1y6mQ[d7]fL4pnZ([Tj9T.jV[h[DZcwaQ(TmWa+DMHK$%BpQZ;gyknef8AV1MBDgV%=4(kwR*pf!,!uE(DRPd.%q[zrm+RhFKj.c8)wiMK$@R6akw0tr)/G*vyJ9]3bPC%m!j#aPCu0{?n{eB,0Lu}GD+$n,Qn*NB,{jwB8Ki@6a]zva,tAduC98L+dN0qn!kDQGhwC1cVq#mY8qKYUnje1YV5YpUPU8,mtWa;SW6vapZiW_:f}45%b%-3Rp_A;16,Xf:vkYgrAi=6!rUwd($wjZU[APm95U9pBaMPeXzfdzGSb?;h$yPaV?Thc_$fVPy*przVVXryhg:A:.1,P4g73+0Zr0{g;H8q#u=ZSc=D98_7K9C#S(1MVRQb;2vF)}zNHcDvf.+?M]mQ0p,NXQ5&6T]h#m:X%ZkBYmVQ=EzJTVtyDkuH8?TZbh/ex4%DDVz=qNLiGAGDn1G-XHZv;_}ZJQ_ji/7GfaTdV.:B$t765;u0?%6LrgD!cC=/z=4pJ@XD%$SuuL7JC@+1S4tFrbvDr!Qt1cr*YKVx/bjpL1xtwPDiv&Av-7Y*ihrZ/wJ+uW7e]ZAR*y(1Qxf9.!}?:TiwDM%];9m;FA%k-qAK2(nT$7rtC=?uhBy%[Q/k8(M{@B,pWm:?G;N4q3biNN8?%EkxS;hBQNnz/%.)H..daHH@Z0]=pG1jivp#n-71]uAMg+1Nr:DX1t_%3S7UNN((Qx:W!rqVKG+C,gP+L=h}7pzW#y70kRT?SDWFS!FW2.]3h+:Y3Ke@{04P!t@:6]!NraZA,0MuXjB6nJ21+]n]YC.((Y/Gu4D-1zZ{h*5BqgXwkukN,iFmptmwzeLp{L44Ex@VD%_MbPC0:z7vLHgchMC}3-$E?M!vR+_V*2AN#hzDWc}.[gXt+#6}W,E41X17&LW7}NUq{;J.{]TtBV?K@Yd0-wE%[*:iMx#x]929a!L2;!rf8u1xX}iyqV+!XQ$T$k6wc6AyG_ujb&bRJq}}8z&bL{/6!/URGjN#G[DC]X]./(Rd78hM&P.Lgr?}#TaWt+z4xw.x+Z!a5dJrLB-mYSCa[@ZJv-u:Bix6S*_-B2YD!FZf3eFWcmDUHYQCjC?B?S@cW#QVrv=VdYSY(WgMQ#-jrrDerhR3=v.+K:e&D1{qk:ZY0$LLP,:L_d*c/G27Y&U+}YvSt/:0)x+[&;/x]Z2AEdiw66.a31TjTYg_*7kmh@kbr:v@y+4!SL15,8*26j)!CXp(9$G%X)0{_3cHt$eR/H{G1(t{akR2ST&G44PR2eF5i+jte9RJ(RZWSLxA#9N_fgS,@BvC@.B,wXq$:C!:S+?,tfBvEWkUHvN/?T+Rz{nyAQfQf3#7vx%5ax]mdy6,r,a]Ad6{ezPv7GGJaLKrP}[fTY4Dug52q{p*]GG&6CCqi}G6SPbX7tSMzz5W3.[aBJ[k,5$:USk:Qg;V(jezFB=]cH(Lt_X(YKp,tgQP3rDE0Fk2C3LAT:tf]p!%}Pf.AzLhmcxD=%JS]f1_5VEyH4C63&1hgUa*p=3aeiC}Kai$wchc=m#Nk&2d==JcJ4dbA:(n/+TiB;HWVcHx,3_!WbZM]rvm0Y=K0j&0MS5N/mxREk[S_!SBHr8[HS4;95peth2:!?DjKGAN8kWTYGm(!ifE6t0z-bLndr*9{mgqHEHY#ed3JxG1J[C9UHq2$RmK_MW2S4xNz%Vu(eMT68rzvbA.bai&MES7hN_wdR;ZjM(n96a9xRN)i],NczG_mrcG+pq)Wv[}wEDS(?*]N%PcB)KaPq[k@?*Tf%Q@fDdJ{hb0J8_{NRn{PjM2@.VpcS9RTc$pc8D*aN;;n;Y.7Gnkrv.JwtAHt1R&UKM2PZ5#Z-*L!J1q=2nLBG!;#uiqzqaUSN.6t.=1@Wj+9t.585zK?9CTri4.5;$8N!YRTZuiAEL+WL8A3b8k5kj[iX[u$]j%c+$*LYX{*H_SJ*#j?]S_/!6{3V;,Ff&kp/*SSS3W1&1(;HQ(Y[M$QZfSkET?c:YANc@Z9&QrYeSMeF@4%49=7?V4mmd]=#Y5kxzFBC:BpEqX2utmBJGdq!1L.@JaWi]fCEh1XY@]pE*Sp?-e_4a6:E141JVuMd{PSA.mNc5j.]+$p_dH}CP%02rP?PpeA2&{0j..Dnt,L{;1,*0_Z,_dv!dW!8vmxNGtZ+6UnHP_#YH9q{?zg(=4{Sw_.,.PkNxzftY1nUWPCZ1}1rEZX8yd%dj0)3(u!{j4BunMRa1+qyZ@TYxNP.E.ZY-fz[i&Wc$qyn69/S.Mi(8:*%}mLS)38h%#FCWZv?D/*gS?8j=A7UDiHymy"
            , null, null)] //tekst duzi od 5000 karaktera
        [Order(6)]
        public async Task IzmeniNovost_assertNotOk(int id_novosti,string? tekst, DateTime? datum, int? id_slucaja)
        {
            Novost n = new Novost();
            n.ID= id_novosti;
            if (datum != null) n.Datum = (DateTime)datum;
            if(tekst!=null)n.Tekst=tekst;
            var actionResult = await controller.IzmeniNovost(n);
            Assert.IsNotInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        [Order(5)]
        public void PreuzmiNovost_assertOk([Range(1,4,1)]int id_novosti)
        {
            var actionResult = controller.PreuzmiNovost(id_novosti);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [TestCase(-5)]
        [Order(8)]
        public void PreuzmiNovost_assertNotFound(int id_novosti)
        {
            var actionResult = controller.PreuzmiNovost(id_novosti);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
    }
}
