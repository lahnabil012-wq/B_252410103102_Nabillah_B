using System;
using System.Collections.Generic;

class Agen
{
public string IdAgen { get; set; }
public string NamaAgen { get; set; }
public int StokAgen { get; set; }

public Agen(string id, string nama, int stok)
{
IdAgen = id;
NamaAgen = nama;
StokAgen = stok;
}

public void TampilInfo()
{
Console.WriteLine("ID Agen   : " + IdAgen);
Console.WriteLine("Nama Agen : " + NamaAgen);
Console.WriteLine("Stok Agen : " + StokAgen);

}
public void TambahStok(int jumlah)
{
if (jumlah > 0)
{
StokAgen += jumlah;
}
}
}



abstract class ProdukGalon
{
private string idProduk;
private string namaProduk;
private int stokGudang;
public string IdProduk
{
get { return idProduk; }
set { idProduk = value; }
}

public string NamaProduk
{
get { return namaProduk; }
set { namaProduk = value; }
}
public int StokGudang
{
get { return stokGudang; }
private set
{
if (value >= 0)
stokGudang = value;
}
}

public ProdukGalon(string id, string nama, int stok)
{
if (stok < 0)
throw new ArgumentException("Stok tidak boleh negatif!");

idProduk = id;
namaProduk = nama;
stokGudang = stok;
}

public bool KurangiStok(int jumlah)
{
if (jumlah <= 0)
{
Console.WriteLine("Jumlah harus lebih dari 0!");
return false;
}

if (jumlah > stokGudang)
{
Console.WriteLine("Stok gudang tidak mencukupi!");
return false;
}

StokGudang = stokGudang - jumlah;
return true;
}

public abstract int HitungBiaya(int jumlah);

public void TampilProduk()
{
Console.WriteLine("ID Produk   : " + IdProduk);
Console.WriteLine("Nama Produk : " + NamaProduk);
Console.WriteLine("Stok Gudang : " + StokGudang);
}
}




class GalonAkua : ProdukGalon
{
public GalonAkua(string id, string nama, int stok)
: base(id, nama, stok)
{

}
public override int HitungBiaya(int jumlah)
{
return jumlah * 20000;
}
}



class GalonBerkeran : ProdukGalon
{
public string JenisKeran { get; set; }

public GalonBerkeran(
string id, string nama, int stok, string keran)
: base(id, nama, stok)
{
JenisKeran = keran;
}

public override int HitungBiaya(int jumlah)
{
return jumlah * 12000;
}
}



interface DapatDidistribusikan
{
void KirimPaket();
}



class DistribusiGalon : DapatDidistribusikan
{
private Agen agenTujuan;
private ProdukGalon produk;
private int jumlah;

public DistribusiGalon(
Agen agen,
ProdukGalon produkGalon,
int jumlahKirim)
    {
agenTujuan = agen;
produk = produkGalon;
jumlah = jumlahKirim;
}

public void KirimPaket()
{
Console.WriteLine("\n=== OPERASI DISTRIBUSI ===");

if (jumlah <= 0)
{
Console.WriteLine(
"Operasi gagal! Jumlah galon harus lebih dari 0.");
return;
}

if (jumlah > produk.StokGudang)
{
Console.WriteLine(
"Operasi gagal! Stok gudang tidak mencukupi.");
return;
        }

bool berhasil = produk.KurangiStok(jumlah);

if (berhasil)
{
agenTujuan.TambahStok(jumlah);

Console.WriteLine(
"Paket berhasil dikirimkan ke agen " +
agenTujuan.NamaAgen);

Console.WriteLine("Jumlah galon: " + jumlah);

Console.WriteLine(
"Total transaksi: Rp" +
produk.HitungBiaya(jumlah));
}
}
}






