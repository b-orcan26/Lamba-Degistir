# Lamba Degistir

  Bu program Wpf'te senkron tcp soket programlamaya örnek olması için oluşturulmuştur. 1 solution içerisinde 2 Wpf projesi bulunur.
Biri Server diğeri Client.Server tarafında lambayı temsil eden bir grafik ve lambayı açıp kapatmaya yarayan bir buton , bir sayi ve
sayiyi random olarak değiştirecek bir buton bulunur.Client tarafındaysa lamba ve sayi butonu bulunur.

  Programdaki amaç serverda bulunan lamba ve sayının hem server hem de client'lar tarafından değiştirilebilmesi ve yapılan değişiklik
sonrası tüm client'ların lamba ve sayının durumu hakkındaki güncel bilgileri alıp arayüzlerindeki ilgili elemanı güncellemesidir.

 Server kısmında program açıldığında arayüz thread'i dışında başka bir thread oluşturulur ve bunun içerisinde gelen istekler dinlenir.
Bir istek geldiğinde isteği yapan socket referansı başka bir thread'e(ConnectedThread) gönderilir ve bu thread içerisinde veri alışverişi 
gerçekleşir.Server kendine gelen tüm istekleri kabul eder, filtreleme yoktur , ve her baglanan istemciye lamba ve sayının o anki durumunu
gönderir.

 İstemci tarafında proje ilk açıldığında yine arayüz thread'i dışındaki bir thread'den belirttiğimiz ip adresi ve portuna bağlanma isteği 
atılır.İstek kabul edildiğinde farklı bir thread'ten veri alışverişi yapılmaya başlanır.İstemci kısmındaki lamba veya sayi butonuna basıl
dığında server'a kendini güncellemesi için bir mesaj gönderilir. Server ilgili alanı günceller ve tüm istemcileri bilgilendirir. Ayrıca 
server üzerindede ilgili alanlar değiştirilirse yine tüm istemcilere gerekli bilgiler gönderilir.

![alt text](https://resimyukle.link/img/DVHzN.jpg)
