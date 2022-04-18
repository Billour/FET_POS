Select t1.prodno, t1.prodname, t1.price, t2.on_hand_qty, T1.IS_POS_DEF_PRICE
from product t1, INV_ON_HAND_CURRENT t2
where t2.store_no='2101' and t1.prodno=t2.prodno and t2.on_hand_qty>0 and t1.imei_flag<>1; --And t1.price<>0;

select * from product where IS_OPEN_PRICE='Y';
edit INV_ON_HAND_CURRENT Where prodno='102000053';

select * from sale_head order by create_dtm desc;
select * from sale_head where posuuid_master='DBA20A3C75DB424AA77C7B69B91B2CCB';
select * from sale_detail where posuuid_master='DCC81855F90F4EE8A48CA843AEE674A8';
select * from paid_detail where posuuid_master='DCC81855F90F4EE8A48CA843AEE674A8';
select * from invoice_head where posuuid_master='CC63942C726645F2BEDF82F24027EAA2';

select * from to_close_head where posuuid_detail='DDD70131E5DF425F9205E29D1E85ACAC';
select * from to_close_item where posuuid_detail='DDD70131E5DF425F9205E29D1E85ACAC';
select * from to_close_item Where promotion_code is not null;

select * from imei where ivrcode='2101' and status='2' and prodno='157200002';
select * from sale_imei_log where imei in ('HLKPM7730315670', 'EKOJC2229096927', 'BRBLP3089926853', 'UNIJJ8665925032');
delete from sale_imei_log where imei in ('HLKPM7730315670', 'EKOJC2229096927', 'BRBLP3089926853');
 
delete from sale_head where posuuid_master='4B3314761AF749FEBECB175F2D2C5E13';
delete from sale_detail where posuuid_master='4B3314761AF749FEBECB175F2D2C5E13';
delete from paid_detail where posuuid_master='4B3314761AF749FEBECB175F2D2C5E13';

select PAY_MODE_ID From PAYMENT_METHOD_SET Where TRUNC(SYSDATE) >= TRUNC(NVL(S_DATE, SYSDATE)) AND TRUNC(SYSDATE) <= TRUNC(NVL(E_DATE, SYSDATE)) Order By PAY_MODE_ID;
select * From PAYMENT_METHOD_SET Where TRUNC(SYSDATE) >= TRUNC(NVL(S_DATE, SYSDATE)) AND TRUNC(SYSDATE) <= TRUNC(NVL(E_DATE, SYSDATE)) Order By PAY_MODE_ID;
