import { apiGet } from "@/src/lib/api";

type Listing = {
  id: string;
  title: string;
  city: string;
  pricePerNight: number;
  maxGuests: number;
};

export default async function ListingPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const p = await params;

  const listing = await apiGet<Listing>(`/listings/${p.id}`);

return (
  <main className="max-w-6xl mx-auto px-6 py-8">
    {/* Title */}
    <div className="mb-4">
      <h1 className="text-2xl font-semibold">{listing.title}</h1>
      <p className="text-gray-600">{listing.city}</p>
    </div>

    {/* Image */}
    <div className="rounded-2xl overflow-hidden bg-gray-100 aspect-[16/9]">
      <img
        src={listing.imageUrl || `https://picsum.photos/seed/${listing.id}/1400/800`}
        alt={listing.title}
        className="w-full h-full object-cover"
      />
    </div>

    {/* Content */}
    <div className="mt-8 grid gap-8 lg:grid-cols-[1fr,380px]">
      {/* Left */}
      <section>
        <h2 className="text-lg font-semibold">Alojamiento entero</h2>
        <p className="text-gray-600 mt-1">
          Hasta {listing.maxGuests} huéspedes · Ubicación: {listing.city}
        </p>

        <div className="mt-6 border-t pt-6">
          <h3 className="font-semibold">Descripción</h3>
          <p className="text-gray-600 mt-2">
            Este es un alojamiento de ejemplo para el portfolio. Aquí
            añadiremos más campos (descripción, amenities e imágenes reales).
          </p>
        </div>
      </section>

      {/* Right (booking card) */}
      <aside className="h-fit rounded-2xl border shadow-sm p-6">
        <div className="flex items-baseline justify-between">
          <div className="text-xl font-semibold">
            {listing.pricePerNight}€ <span className="text-sm font-normal text-gray-600">noche</span>
          </div>
          <div className="text-sm flex items-center gap-1">
            ⭐ <span>4.8</span>
          </div>
        </div>

        <button
          className="mt-6 w-full py-3 rounded-lg text-white font-medium"
          style={{ background: "var(--airbnb-red)" }}
        >
          Reservar
        </button>

        <p className="text-xs text-gray-500 mt-3">
          Todavía no cobraremos nada. Esto es UI.
        </p>
      </aside>
    </div>
  </main>
);

}

