import SearchBarClient from "@/src/components/SearchBarClient";


export default function SiteHeader() {
  return (
    <header className="sticky top-0 z-20 bg-white border-b">
      <div className="max-w-6xl mx-auto px-6">
        {/* Top row */}
        <div className="h-20 flex items-center justify-between">
          {/* Left: logo */}
          <div className="flex items-center gap-2">
            <div
              className="h-9 w-9 rounded-full grid place-items-center text-white font-bold"
              style={{ background: "var(--airbnb-red)" }}
            >
              A
            </div>
            <span className="font-semibold tracking-tight">airbnb</span>
          </div>

          {/* Center: tabs */}
          <nav className="hidden md:flex items-center gap-10 text-sm font-medium">
            <a className="flex items-center gap-2 border-b-2 border-black pb-2" href="#">
              🏠 <span>Alojamientos</span>
            </a>
            <a className="flex items-center gap-2 text-gray-500 hover:text-black" href="#">
              🎈 <span>Experiencias</span>
            </a>
            <a className="flex items-center gap-2 text-gray-500 hover:text-black" href="#">
              🧑‍🔧 <span>Servicios</span>
            </a>
          </nav>

          {/* Right: actions */}
          <div className="flex items-center gap-3">
            <button className="hidden md:block text-sm font-medium px-3 py-2 rounded-full hover:bg-gray-100">
              Converteix-te en amfitrió
            </button>
            <button className="h-10 w-10 rounded-full hover:bg-gray-100 grid place-items-center">
              🌐
            </button>
            <button className="h-10 w-10 rounded-full border shadow-sm hover:shadow grid place-items-center">
              ☰
            </button>
          </div>
        </div>

        {/* Search bar row (desktop like Airbnb) */}
        <div className="pb-4 hidden md:flex justify-center">
            <SearchBarClient />
        </div>
      </div>
    </header>
  );
}
