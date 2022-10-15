RabbitMQ kendisine g�nderilen mesajlar� direkt olarak olu�turulan kuyru�a eklemekte ve consumerlar ise bu mesajlar� s�ras�yla t�ketmektedir. 
S�re� bu �ekilde ilerlerken birden fazla consumer�n oldu�u durumlarda mesaj t�ketiminin nas�l bir algoritma taraf�ndan i�lenece�ini ve hangi 
consumer�n ne kadar mesaj� i�leyece�ini bildirmemiz gerekmektedir. ��te bu durumda devreye exchangeler girmekte ve t�r�ne g�re exchange ile 
kuyru�a g�nderilen mesajlar t�r�n getirdi�i i�leme mant���na g�re t�keticilere g�nderilerek i�letilmektedir. �lk olarak t�m consumerlara 
mesajlar�n iletilmesini sa�layan Fanout Exchange yap�lanmas�n� inceleyece�iz.

Exchange Nedir?

RabbitMQ servislerine g�nderilen mesajlar exchange taraf�ndan kar��lanmakta ve exchange�in t�r�ne g�re ilgili kuyru�a iletilerek, dahil 
edilmekte ve bu kuyru�u takip eden t�m consumerlar taraf�ndan mesajlar t�ketilmekte bir ba�ka deyi�le i�lenmektedir.
Burada consumerlar taraf�ndan kuyruklar�n takip edilmesi durumunu, mesaj� yay�nlayan(publisher) bu yay�nlara abone/subscribe olan 
consumerlar�n olmas�ndan dolay� Publish/Subscribe Pattern olarak ifade etmekteyiz.

Fanout Exchange ��leyi�i Nas�ld�r?

Fanout exchange ald��� mesaj� direkt olarak t�m kuyruklara da��tmakta ve b�ylece kuyruklar� dinleyen t�m consumerlara i�letmektedir.
Genellikle a�a��daki durumlara benzer senaryolarda tercih edilebilir.
	- G�ncel skorlar�n t�m oyunculara bildirilmesi,
	- Hava durumu bilgisinin t�m kanallarda yay�nlanmas�,
	- T�m birimlere %n�lik bir ikramiyede bulunulmas�

