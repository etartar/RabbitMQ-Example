Topic Exchange�de at�lan mesajlar�n routing key de�eri .(nokta) operat�r� kullan�larak formatland�r�lmakta ve bu formattaki isimlerde yap�lan 
filtrelemelere g�re uygun d��en kuyruklara mesajlar g�nderilmektedir. 

�rnek kullan�mlar� : 

- �usa.news�
- �usa.weather�
- �europe.news�
- �europe.weather�

Yukar�daki routing key de�erlerine sahip olan mesajlar� hedef kuyru�a g�nderebilmek i�in olu�turdu�umuz kuyruklarda a�a��dakine benzer bir 
filtreleme yap�lmas� yeterlidir.

�usa.news� -> Routing key de�eri �usa.news� olan mesajlar bu kuyru�a girecektir.
�*.weather� -> �*<�nemli de�il>.weather� olan mesajlar bu kuyru�a girecektir.
	- 'usa.weather', 'europe.weather'
�#.news� -> �#< ba�� �nemli de�il>.news� olan mesajlar bu kuyru�a girecektir.
	- 'usa.news', 'europe.news'
�usa.#� -> �usa.#< sonu �nemli de�il>� olan mesajlar bu kuyru�a girecektir.
	- 'usa.weather', 'usa.news'
�europe.*� -> �europe.*<�nemli de�il>� olan mesajlar bu kuyru�a girecektir.
	- 'europe.news', 'europe.weather'
�#� -> �#< hi�biri �nemli de�il>� olan mesajlar bu kuyru�a girecektir.
	- 'europe.news', 'usa.weather', 'europe.weather', 'usa.news'

�rnek olarak silahl� kuvvetler hiyerar�isine uygun bir �ekilde g�revlendirme senaryosu benimseyip mesajlar�m�z� ilgili r�tbelere �zel
g�rev bar�nd�ran kuyruklara iletece�iz.

Topic exchange�e uygun formatta �Asker.Subay.[Tegmen � Yuzbasi � Binbasi]� routing keyi olu�turulmaktad�r. Burada senaryo gere�i olu�turulan 
100 adet g�revin her birini �Te�men�, yar�s�n� �Y�zba��� ve hatr� say�l�r az�nl�ktaki kadar�n� �Binba��� r�tbeli askerlerin �stlenece�i 
�ekilde bir algoritma olu�turulmu� bulunmaktad�r ve her bir g�rev mesaj olarak ilgili r�tbeyi temsil eden formattaki routing key de�erine 
sahip kuyru�a g�nderilmektedir.