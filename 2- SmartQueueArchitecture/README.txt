Bir kuyruk mimarisinin ak�ll� olabilmesi i�in, edindi�i mesajlar�n g�venli bir �ekilde tutulup, e�it bir da��l�mla t�ketilmesi
ve t�m bu i�lemler neticesinde sunucunun mesaj�n i�lendi�ine dair haberdar edilmesi gerekmektedir. Bu t�r yap�lanmaya ak�ll�
kuyruk mimarisi denir

Bir kuyruk yap�s�n�n ak�ll� olabilmesi i�in a�a��daki �zellikleri uyguluyor olmas� gerekmektedir.

1 - Mesajlar�n Dayan�kl��� (Message Durability): RabbitMQ istance'�na g�nderilen mesajlar�n g�venli�ini sa�layarak olas� restart
durumlar�nda kal�c�l��� korumak i�in Publisher ilgili mesaj� g�ndermeden �nce mesaj�n g�venirlili�i sa�lanmal�d�r. Bunun i�in
QueueDeclare k�sm�nda durable:true parametresi true olarak ayarlan�r ve IBasicProperties'de persistent true olarak atanarak 
RabbitMQ'da fiziksel kayda al�n�r ve kal�c�l��� sa�lan�r. B�ylece mesaj g�venli�ide sa�lanm�� olur. 

2 - E�it Da��l�m (Fair Dispatch): Bir kuyru�u t�ketmek i�in birden fazla consumer olabilir. B�yle bir durumda t�m consumerlara 
mesajlar e�it da��t�lmal�d�r. Aksi taktirde i�lemci g�c� vs. gibi donan�msal �zellikleri daha a��r basan consumerlar k�sa zamanda 
�ok mesaj i�leyebildikleri i�in daha yo�un �al��mak zorunda kalabilirler. �BasicQos� metodu ile port �nceli�i anlam�na gelen QoS servisi 
ile consumer �nceli�i e�itlenmekte ve b�ylece e�it da��l�m sa�lanmaktad�r. 
�BasicQos� metodunun;
	- prefetchSize parametresi : Mesaj boyutunu ifade eder. 0(s�f�r) diyerek ilgilenmedi�imizi belirtiyoruz.
	- prefetchCount parametresi : Da��t�m adetini ifade eder.
	- global parametresi : true ise, t�m consumerlar�n ayn� anda prefetchCount parametresinde belirtilen de�er kadar mesaj t�ketebilece�ini 
	ifade eder. false de�eri ise, her bir consumer�n bir i�leme s�resinde di�er consumerlardan ba��ms�z bir �ekilde ka� mesaj al�p i�leyece�ini 
	belirtir.

3 - Mesajlar�n Al�nd� Haberi (Message Acknowledgement): Kuyruktaki mesaj consumer taraf�ndan ba�ar�yla i�lendikten sonra sunucu 
bilgilendirilmelidir. �BasicAck� fonksiyonu ile bu i�lemi ger�ekle�tirerek kuyruktan mesaj�n silinmesi sa�lanmaktad�r.