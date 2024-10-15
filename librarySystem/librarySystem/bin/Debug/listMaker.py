def numaralandir_ve_temizle(dosya_adi):
    with open(dosya_adi, 'r', encoding='utf-8') as dosya:
        satirlar = dosya.readlines()

    yeni_satirlar = []
    sayac = 13

    for satir in satirlar:
        temiz_satir = satir.strip()
        if temiz_satir:  # Boş satırları atla
            yeni_satirlar.append(f"{sayac} - {temiz_satir}\n")
            sayac += 1

    with open(dosya_adi, 'w', encoding='utf-8') as dosya:
        dosya.writelines(yeni_satirlar)

# Kodu çalıştırmak için fonksiyonu çağır:
numaralandir_ve_temizle("kitapList-Pre.txt")
