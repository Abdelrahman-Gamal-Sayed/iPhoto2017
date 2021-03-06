select CUST_SERV.Order_Number,Customer.English_Name,SERVIES_TYP.ANAME,SERVIES.NAME,CUST_SERV.QTY,CUST_SERV.Price_servies,CUST_SERV.Total_Price,CUST_SERV.Created_by,CUST_SERV.Created_Date
from CUST_SERV , Customer, SERVIES_TYP, SERVIES
where SERVIES.Code = CUST_SERV.SERV_NUM
and SERVIES_TYP.Code = SERVIES.SYRVIES_TYP_NUM
and CUST_SERV.SERV_TYP_NUM = SERVIES_TYP.Code
and CUST_SERV.CUST_ID = Customer.ID
and CUST_SERV.SERV_TYP_NUM like '%%' and CUST_SERV.SERV_NUM like '%%' and CUST_SERV.Created_Date between '2000-1-1' and '3000-1-1'



select sum(price_servies) , count(Order_Number) from CUST_SERV where CUST_SERV.SERV_TYP_NUM like '%%' and CUST_SERV.SERV_NUM like '%%' and CUST_SERV.Created_Date between '2000-1-1' and '3000-1-1'