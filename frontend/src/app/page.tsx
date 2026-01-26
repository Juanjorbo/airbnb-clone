import SiteHeader from "@/src/components/SiteHeader";
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

function SearchPill({ total }: { total: number }) {
  return (
    <div className="md:hidden max-w-6xl mx-auto px-6 pt-5">
      <div className="rounded-2xl border shadow-sm p-4 flex items-center gap-3">
        <div
          className="h-10 w-10 rounded-full grid place-items-center text-white"
          style={{ background: "var(--airbnb-red)" }}
        >
          🔍
        </div>
        <div className="flex-1">
          <div className="font-medium">¿A dónde?</div>
          <div className="text-sm text-gray-500">
            {total} alojamientos · Cualquier fecha · Invitados
          </div>
        </div>
      </div>
    </div>
  );
}

function ListingCard({ l }: { l: ListingItem }) {
  return (
    <div className="group cursor-pointer">
      <div className="relative overflow-hidden rounded-2xl bg-gray-100 aspect-[4/3]">
        <img
          src={`https://picsum.photos/seed/${l.id}/900/675`}
          alt={l.title}
          className="h-full w-full object-cover transition-transform duration-300 group-hover:scale-105"
        />

        <button
          className="absolute top-3 right-3 h-8 w-8 rounded-full bg-white/90 grid place-items-center shadow"
          aria-label="Favorite"
        >
          🤍
        </button>

        <div className="absolute top-3 left-3">
          <span className="text-xs bg-white/90 px-3 py-1 rounded-full shadow">
            Recomendación
          </span>
        </div>
      </div>

      <div className="mt-3">
        <div className="flex items-start justify-between gap-2">
          <div className="font-medium leading-tight line-clamp-1">{l.title}</div>
          <div className="text-sm flex items-center gap-1">
            ⭐ <span>4.8</span>
          </div>
        </div>

        <div className="text-sm text-gray-500 line-clamp-1">{l.city}</div>

        <div className="text-sm mt-1">
          <span className="font-semibold">{l.pricePerNight}€</span>{" "}
          <span className="text-gray-600">noche</span>
          <span className="text-gray-400"> · </span>
          <span className="text-gray-600">{l.maxGuests} huéspedes</span>
        </div>
      </div>
    </div>
  );
}

export default async function Home() {
  const data = await apiGet<ListingsResponse>("/listings");

  return (
    <main className="bg-white">
      <SiteHeader />
      <SearchPill total={data.total} />

      <section className="max-w-6xl mx-auto px-6 py-8">
        <div className="flex items-center justify-between">
          <h1 className="text-xl font-semibold">Alojamientos</h1>
          <div className="text-sm text-gray-500">{data.total} resultados</div>
        </div>

        <div className="grid gap-6 mt-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
          {data.items.map((l) => (
            <ListingCard key={l.id} l={l} />
          ))}
        </div>

        {data.items.length === 0 && (
          <div className="text-sm text-gray-600 mt-6">
            No hay listings todavía. Crea uno desde Swagger y refresca.
          </div>
        )}
      </section>
    </main>
  );
}
