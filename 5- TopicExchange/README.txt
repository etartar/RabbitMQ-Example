Topic Exchange’de atýlan mesajlarýn routing key deðeri .(nokta) operatörü kullanýlarak formatlandýrýlmakta ve bu formattaki isimlerde yapýlan 
filtrelemelere göre uygun düþen kuyruklara mesajlar gönderilmektedir. 

Örnek kullanýmlarý : 

- ‘usa.news’
- ‘usa.weather’
- ‘europe.news’
- ‘europe.weather’

Yukarýdaki routing key deðerlerine sahip olan mesajlarý hedef kuyruða gönderebilmek için oluþturduðumuz kuyruklarda aþaðýdakine benzer bir 
filtreleme yapýlmasý yeterlidir.

‘usa.news’ -> Routing key deðeri ‘usa.news’ olan mesajlar bu kuyruða girecektir.
‘*.weather’ -> ‘*<önemli deðil>.weather’ olan mesajlar bu kuyruða girecektir.
	- 'usa.weather', 'europe.weather'
‘#.news’ -> ‘#< baþý önemli deðil>.news’ olan mesajlar bu kuyruða girecektir.
	- 'usa.news', 'europe.news'
‘usa.#’ -> ‘usa.#< sonu önemli deðil>’ olan mesajlar bu kuyruða girecektir.
	- 'usa.weather', 'usa.news'
‘europe.*’ -> ‘europe.*<önemli deðil>’ olan mesajlar bu kuyruða girecektir.
	- 'europe.news', 'europe.weather'
‘#’ -> ‘#< hiçbiri önemli deðil>’ olan mesajlar bu kuyruða girecektir.
	- 'europe.news', 'usa.weather', 'europe.weather', 'usa.news'

Örnek olarak silahlý kuvvetler hiyerarþisine uygun bir þekilde görevlendirme senaryosu benimseyip mesajlarýmýzý ilgili rütbelere özel
görev barýndýran kuyruklara ileteceðiz.

Topic exchange’e uygun formatta “Asker.Subay.[Tegmen – Yuzbasi – Binbasi]” routing keyi oluþturulmaktadýr. Burada senaryo gereði oluþturulan 
100 adet görevin her birini ‘Teðmen’, yarýsýný ‘Yüzbaþý’ ve hatrý sayýlýr azýnlýktaki kadarýný ‘Binbaþý’ rütbeli askerlerin üstleneceði 
þekilde bir algoritma oluþturulmuþ bulunmaktadýr ve her bir görev mesaj olarak ilgili rütbeyi temsil eden formattaki routing key deðerine 
sahip kuyruða gönderilmektedir.