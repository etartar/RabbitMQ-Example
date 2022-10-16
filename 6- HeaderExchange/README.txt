Header Exchange, Topic Exchange‘in key – value olarak tanýmlanan halidir diyebiliriz. Yani anlayacaðýnýz gönderilen mesajlarýn routing key 
deðerini topic exchange’de olduðu gibi .(nokta) ile deðilde, ilgili mesajýn header kýsmýndan verilen key – value formatýnda deðer ile 
oluþturarak, bu keylerle eþleþen kuyruklara ileten bir exchange türüdür.

Burada header’a verilen ‘x-match’ key deðerine dikkatinizi çekerim. ‘x-match’; ‘all’ veya ‘any’ olmak üzere iki deðer alabilen ve fonksiyonel 
olarak header deðerlerinden herhangi biriyle yahut hepsiyle eþleþme durumunu kontrol edip duruma göre ilgili consumerý ilgili kuyruða 
subscribe eden bir niteliðe sahiptir.