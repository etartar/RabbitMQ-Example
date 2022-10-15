RabbitMQ kendisine gönderilen mesajlarý direkt olarak oluþturulan kuyruða eklemekte ve consumerlar ise bu mesajlarý sýrasýyla tüketmektedir. 
Süreç bu þekilde ilerlerken birden fazla consumerýn olduðu durumlarda mesaj tüketiminin nasýl bir algoritma tarafýndan iþleneceðini ve hangi 
consumerýn ne kadar mesajý iþleyeceðini bildirmemiz gerekmektedir. Ýþte bu durumda devreye exchangeler girmekte ve türüne göre exchange ile 
kuyruða gönderilen mesajlar türün getirdiði iþleme mantýðýna göre tüketicilere gönderilerek iþletilmektedir. Ýlk olarak tüm consumerlara 
mesajlarýn iletilmesini saðlayan Fanout Exchange yapýlanmasýný inceleyeceðiz.

Exchange Nedir?

RabbitMQ servislerine gönderilen mesajlar exchange tarafýndan karþýlanmakta ve exchange’in türüne göre ilgili kuyruða iletilerek, dahil 
edilmekte ve bu kuyruðu takip eden tüm consumerlar tarafýndan mesajlar tüketilmekte bir baþka deyiþle iþlenmektedir.
Burada consumerlar tarafýndan kuyruklarýn takip edilmesi durumunu, mesajý yayýnlayan(publisher) bu yayýnlara abone/subscribe olan 
consumerlarýn olmasýndan dolayý Publish/Subscribe Pattern olarak ifade etmekteyiz.

Fanout Exchange Ýþleyiþi Nasýldýr?

Fanout exchange aldýðý mesajý direkt olarak tüm kuyruklara daðýtmakta ve böylece kuyruklarý dinleyen tüm consumerlara iþletmektedir.
Genellikle aþaðýdaki durumlara benzer senaryolarda tercih edilebilir.
	- Güncel skorlarýn tüm oyunculara bildirilmesi,
	- Hava durumu bilgisinin tüm kanallarda yayýnlanmasý,
	- Tüm birimlere %n’lik bir ikramiyede bulunulmasý

