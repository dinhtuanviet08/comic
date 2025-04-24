namespace ComicSystem.Models
{
    public class RentalDetail
{
    public int RentalDetailId { get; set; }
    public int RentalId { get; set; }
    public int ComicBookId { get; set; }
    public int Quantity { get; set; }
    public double PricePerDay { get; set; }

    public Rental Rental { get; set; }
    public ComicBook ComicBook { get; set; }
}

}
