Header Exchange, Topic Exchange�in key � value olarak tan�mlanan halidir diyebiliriz. Yani anlayaca��n�z g�nderilen mesajlar�n routing key 
de�erini topic exchange�de oldu�u gibi .(nokta) ile de�ilde, ilgili mesaj�n header k�sm�ndan verilen key � value format�nda de�er ile 
olu�turarak, bu keylerle e�le�en kuyruklara ileten bir exchange t�r�d�r.

Burada header�a verilen �x-match� key de�erine dikkatinizi �ekerim. �x-match�; �all� veya �any� olmak �zere iki de�er alabilen ve fonksiyonel 
olarak header de�erlerinden herhangi biriyle yahut hepsiyle e�le�me durumunu kontrol edip duruma g�re ilgili consumer� ilgili kuyru�a 
subscribe eden bir niteli�e sahiptir.