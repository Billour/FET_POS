//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	Internet 相關驗證
//---------------------------------------------------------------------------- 
namespace Advtek.Utility
{
    public class Check_Internet
    {
        #region TopLevelDomain 最高層網域 (Top Level Domain) 名稱定義
        // 此處共計251個資料，如有變更時，請自行增減
        // 資料可由 Internet Assigned Numbers Authority (IANA，http://www.iana.org) 取得 
        public string[] TopLevelDomain = new string[27] {


		".com.org.net.edu.gov.mil.int",
		".ac.ad.ae.af.ag.ai.al.am.an.ao.aq.ar.as.at.au.aw.az",
		".ba.bd.bb.be.bf.bg.bh.bi.bj.bm.bn.bo.br.bs.bt.bv.bw.by.bz",
		".ca.cc.cd.cf.cg.ch.ci.ck.cl.cm.cn.co.cr.cu.cv.cx.cy.cz",
		".de.dj.dk.dm.do.dz",
		".ec.ee.eg.eh.er.es.et",
		".fi.fj.fk.fm.fo.fr",
		".ga.gb.gd.gf.gg.gq.ge.gh.gi.gl.gm.gn.gp.gr.gs.gt.gu.gw.gy",
		".hk.hm.hn.hr.ht.hu",
		".id.ie.il.im.in.io.iq.ir.is.it",
		".je.jm.jo.jp",
		".ke.kg.kh.ki.km.kn.kp.kr.kw.ky.kz",
		".la.lc.lb.li.lk.lr.ls.lt.lu.lv.ly",
		".ma.mc.md.mg.mh.mk.ml.mm.mn.mo.mp.mq.mr.ms.mt.mu.mv.mw.mx.my.mz",
		".na.nc.ne.nf.ng.ni.nl.no.np.nr.nu.nz",
		".om",
		".pa.pe.pf.pg.ph.pk.pl.pm.pn.pt.pr.pw.py",
		".qa",
		".re.ro.ru.rw",
		".sa.sb.sc.sd.se.sg.sh.si.sj.sk.sl.sm.sn.so.sr.st.sv.sy.sz",
		".tc.td.tf.tg.th.tj.tk.tm.tn.to.tp.tr.tt.tv.tw.tz",
		".ua.ug.uk.um.us.uy.uz",
		".va.vc.ve.vg.vi.vn.vu",
		".ws.wf",
		".",
		".ye.yt.yu",
		".za.zm.zr.zw"
	};
        #endregion

        #region Check_Email() 驗證電子郵件信箱
        public int Check_Email(string strEmail)
        {
            int intPos = 0, rtn_value = 0, sCnt = 0;
            string strHost = "";
            string strInvalidChars = "";

            strEmail = strEmail.Trim();

            // 1 字串的長度是否小於 5 碼 (Email 最少要有 A@B.C，五個字所組成)
            sCnt = strEmail.Length;		// 字串長度

            if (sCnt < 5)
            {
                rtn_value = 1;
            }

            // 2 檢查字串中含有不合法的字元。 
            if (rtn_value == 0)
            {
                strInvalidChars = "!#$%^&*()=+{}[]|\\;:'/?>,< ";

                for (intPos = 0; intPos < sCnt; intPos++)
                {
                    if (strInvalidChars.Contains(strEmail.Substring(intPos, 1)))
                    {
                        rtn_value = 2;
                        intPos = sCnt;		// 結束迴圈
                    }
                }
            }

            // 3 檢查字串的首字或尾字是否含有"@"字元。 
            if (rtn_value == 0)
            {
                intPos = strEmail.IndexOf("@") + 1;
                if (intPos == 1 || intPos == sCnt || intPos == 0)
                    rtn_value = 3;
            }

            // 4 檢查字串中含有第二個"@"字元。 	
            if (rtn_value == 0)
            {
                // 取出郵件伺服器位址，即如一般 "hinet.net"
                strHost = strEmail.Substring(intPos, sCnt - intPos);

                if (strHost.IndexOf("@") > -1)
                    rtn_value = 4;
            }

            // 5 郵件伺服器位位址驗證。 
            if (rtn_value == 0)
                rtn_value = Check_Host(strHost.ToLower());

            return rtn_value;
        }
        #endregion

        #region Check_Host() 驗證伺服器位址(網域名稱)
        public int Check_Host(string strHost)
        {
            int rtn_value = 0, ckint = 0, intCnt = 0, addtype = 0;
            string[] strSplit = null;

            // 11 檢查字串中是否含有"."字元。 
            if (!strHost.Contains("."))
            {
                rtn_value = 11;
            }

            // 確認位址的二種型態
            //		a. ＩＰ型態，如 "212.212.212.212"
            //		b. 網名型態，如 "hinet.net"
            if (rtn_value == 0)
            {
                strSplit = strHost.Split('.');

                // 判斷陣列是否分成四個字串 
                if (strSplit.Length == 4)
                {
                    // 若為四個字串，則先預定為ＩＰ型態。 
                    addtype = 1;

                    // 檢查四個字串之中，是否為數字。 
                    for (intCnt = 0; intCnt < 4; intCnt++)
                    {
                        if (!int.TryParse(strSplit[intCnt], out ckint))
                        {
                            addtype = 2;		// 有非數字存在，設成網名型態
                            intCnt = 4;			// 結束迴圈
                        }
                    }

                    // ＩＰ型態：
                    // 第一組ＩＰ要在 0~239 之間，二、三、四組ＩＰ要在 0~255 之間
                    if (addtype == 1)
                    {
                        for (intCnt = 0; intCnt < 4; intCnt++)
                        {
                            if (intCnt == 0)
                            {
                                ckint = int.Parse(strSplit[intCnt]);
                                if (ckint > 239 || ckint < 0)
                                {
                                    rtn_value = 12;
                                    intCnt = 4;		// 結束迴圈
                                }
                            }
                            else
                            {
                                ckint = int.Parse(strSplit[intCnt]);
                                if (ckint > 255 || ckint < 0)
                                {
                                    rtn_value = 13;
                                    intCnt = 4;		// 結束迴圈
                                }
                            }
                        }
                    }
                }
                else
                    addtype = 2;

                if (addtype == 2 && rtn_value == 0)
                {
                    // 網名型態：
                    // 檢查最高級層網域，即檢查最後一字串是否符合www.iana.org規字的字串。 
                    // 如 "seed.net.tw" 的最後一字串為 "tw"。 
                    // 如 "lsc.lu@msa.hinet.net"的最後一字串為 "net"。
                    rtn_value = Check_TopLevelDomain(strSplit[strSplit.Length - 1]);
                }
            }

            return rtn_value;
        }
        #endregion

        #region Check_TopLevelDomain() 驗證最高層網域 (Top Level Domain)
        public int Check_TopLevelDomain(string strDomain)
        {
            int rtn_value = 21;
            int iCnt = 0;

            strDomain = strDomain.ToLower();

            if (strDomain != "")
            {
                // 檢查輸入字串是否存在於規定字串裡 
                for (iCnt = 0; iCnt < 27; iCnt++)
                {
                    if (TopLevelDomain[iCnt].Contains(strDomain))
                    {
                        rtn_value = 0;
                        iCnt = 27;	// 停止迴圈
                    }
                }
            }
            return rtn_value;
        }
        #endregion
    }
}