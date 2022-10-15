Bir kuyruk mimarisinin akýllý olabilmesi için, edindiði mesajlarýn güvenli bir þekilde tutulup, eþit bir daðýlýmla tüketilmesi
ve tüm bu iþlemler neticesinde sunucunun mesajýn iþlendiðine dair haberdar edilmesi gerekmektedir. Bu tür yapýlanmaya akýllý
kuyruk mimarisi denir

Bir kuyruk yapýsýnýn akýllý olabilmesi için aþaðýdaki özellikleri uyguluyor olmasý gerekmektedir.

1 - Mesajlarýn Dayanýklýðý (Message Durability): RabbitMQ istance'ýna gönderilen mesajlarýn güvenliðini saðlayarak olasý restart
durumlarýnda kalýcýlýðý korumak için Publisher ilgili mesajý göndermeden önce mesajýn güvenirliliði saðlanmalýdýr. Bunun için
QueueDeclare kýsmýnda durable:true parametresi true olarak ayarlanýr ve IBasicProperties'de persistent true olarak atanarak 
RabbitMQ'da fiziksel kayda alýnýr ve kalýcýlýðý saðlanýr. Böylece mesaj güvenliðide saðlanmýþ olur. 

2 - Eþit Daðýlým (Fair Dispatch): Bir kuyruðu tüketmek için birden fazla consumer olabilir. Böyle bir durumda tüm consumerlara 
mesajlar eþit daðýtýlmalýdýr. Aksi taktirde iþlemci gücü vs. gibi donanýmsal özellikleri daha aðýr basan consumerlar kýsa zamanda 
çok mesaj iþleyebildikleri için daha yoðun çalýþmak zorunda kalabilirler. ‘BasicQos’ metodu ile port önceliði anlamýna gelen QoS servisi 
ile consumer önceliði eþitlenmekte ve böylece eþit daðýlým saðlanmaktadýr. 
‘BasicQos’ metodunun;
	- prefetchSize parametresi : Mesaj boyutunu ifade eder. 0(sýfýr) diyerek ilgilenmediðimizi belirtiyoruz.
	- prefetchCount parametresi : Daðýtým adetini ifade eder.
	- global parametresi : true ise, tüm consumerlarýn ayný anda prefetchCount parametresinde belirtilen deðer kadar mesaj tüketebileceðini 
	ifade eder. false deðeri ise, her bir consumerýn bir iþleme süresinde diðer consumerlardan baðýmsýz bir þekilde kaç mesaj alýp iþleyeceðini 
	belirtir.

3 - Mesajlarýn Alýndý Haberi (Message Acknowledgement): Kuyruktaki mesaj consumer tarafýndan baþarýyla iþlendikten sonra sunucu 
bilgilendirilmelidir. ‘BasicAck’ fonksiyonu ile bu iþlemi gerçekleþtirerek kuyruktan mesajýn silinmesi saðlanmaktadýr.