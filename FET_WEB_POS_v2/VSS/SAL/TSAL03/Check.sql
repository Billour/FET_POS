Select t1.prodno, t1.price, t2.on_hand_qty
from product t1, INV_ON_HAND_CURRENT t2
where t2.store_no='2101' and t1.prodno=t2.prodno and t2.on_hand_qty>0 and t1.imei_flag=1 And t1.price<>0;

select * From product;
select * from sale_head order by create_dtm desc;
select * from sale_head where posuuid_master='1F928E6CAB3C475EA382F8835B183870';
select * from sale_detail where posuuid_master='034507325EC54D18821414B06586228E';
select * from paid_detail where posuuid_master='034507325EC54D18821414B06586228E';
select * from invoice_head where posuuid_master='034507325EC54D18821414B06586228E';

select * from imei where ivrcode='2101' and status='2' and prodno='150300095';

select * from sale_imei_log where imei in ('HLKPM7730315670', 'EKOJC2229096927', 'BRBLP3089926853', 'UNIJJ8665925032');
delete from sale_imei_log where imei in ('HLKPM7730315670', 'EKOJC2229096927', 'BRBLP3089926853');
 
