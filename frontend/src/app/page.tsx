import { apiGet } from "@/src/lib/api";

type ListingItem = {
  id: string;
  title: string;
  city: string;
  pricePerNight: number;
  maxGuests: number;
};

type ListingsResponse = {
  total: number;
  page: number;
  pageSize: number;
  items: ListingItem[];
};

export default async function Home() {
  const data = await apiGet<ListingsResponse>("/listings");

  return (
    <main className="max-w-5xl mx-auto p-6">
      <h1 className="text-2xl font-semibold">Airbnb Clone</h1>
      <p className="text-sm text-gray-600 mt-1">
        Total listings: {data.total}
      </p>

      <div className="grid gap-4 mt-6 sm:grid-cols-2 lg:grid-cols-3">
        {data.items.map((l) => (
          <div key={l.id} className="rounded-xl border p-4">
            <div className="font-medium">{l.title}</div>
            <div className="text-sm text-gray-600">{l.city}</div>
            <div className="mt-2 text-sm">
              <span className="font-semibold">{l.pricePerNight}€</span> / noche ·{" "}
              {l.maxGuests} huéspedes
            </div>
          </div>
        ))}

        {data.items.length === 0 && (
          <div className="text-sm text-gray-600">
            No hay listings todavía. Crea uno desde Swagger y vuelve a refrescar.
          </div>
        )}
      </div>
    </main>
  );
}
