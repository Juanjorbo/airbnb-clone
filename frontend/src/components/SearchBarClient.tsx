import { toQueryString } from "@/src/lib/urlFilters";

"use client";

import { useRouter, useSearchParams } from "next/navigation";
import { useState } from "react";

export default function SearchBarClient() {
  const router = useRouter();
  const sp = useSearchParams();

    const [city, setCity] = useState(sp.get("city") ?? "");
    const [guests, setGuests] = useState(sp.get("guests") ?? "");
    const [minPrice, setMinPrice] = useState(sp.get("minPrice") ?? "");
    const [maxPrice, setMaxPrice] = useState(sp.get("maxPrice") ?? "");
    const [isSearching, setIsSearching] = useState(false);


function onSearch() {
  setIsSearching(true);

  const qs = new URLSearchParams();
  if (city.trim()) qs.set("city", city.trim());
  if (guests) qs.set("guests", guests);
  if (minPrice) qs.set("minPrice", minPrice);
  if (maxPrice) qs.set("maxPrice", maxPrice);

  const nextUrl = qs.toString() ? `/?${qs.toString()}` : `/`;
  router.push(nextUrl);

  setTimeout(() => setIsSearching(false), 500);
}


  return (
    <div className="w-full max-w-3xl rounded-full border bg-white shadow-md px-6 py-3 flex items-center gap-6">
      {/* City */}
      <div className="flex-1">
        <div className="text-xs font-semibold">Where</div>
        <input
          autoComplete="off"
          className="w-full text-sm outline-none text-gray-700 placeholder:text-gray-400"
          placeholder="Search destinations"
          value={city}
          onChange={(e) => setCity(e.target.value)}
        />
      </div>

      <div className="h-8 w-px bg-gray-200" />

      {/* Price */}
      <div className="flex-1">
        <div className="text-xs font-semibold">Price</div>
        <div className="flex gap-3">
          <input
            className="w-24 text-sm outline-none text-gray-700 placeholder:text-gray-400"
            placeholder="Min"
            inputMode="numeric"
            value={minPrice}
            onChange={(e) => setMinPrice(e.target.value)}
          />
          <input
            className="w-24 text-sm outline-none text-gray-700 placeholder:text-gray-400"
            placeholder="Max"
            inputMode="numeric"
            value={maxPrice}
            onChange={(e) => setMaxPrice(e.target.value)}
          />
        </div>
      </div>

      <div className="h-8 w-px bg-gray-200" />

      {/* Guests */}
      <div className="flex-1 flex items-center justify-between gap-3">
        <div>
          <div className="text-xs font-semibold">Guests</div>
          <input
            className="w-32 text-sm outline-none text-gray-700 placeholder:text-gray-400"
            placeholder="Add guests"
            inputMode="numeric"
            value={guests}
            onChange={(e) => setGuests(e.target.value)}
          />
        </div>

        <button
        onClick={onSearch}
        disabled={isSearching}
        className={`h-12 w-12 rounded-full text-white grid place-items-center transition
            ${isSearching ? "opacity-80 scale-95" : "hover:scale-105 active:scale-95"}`}
        style={{ background: "var(--airbnb-red)" }}
        aria-label="Search"
        >
        <span className={isSearching ? "animate-pulse" : ""}>
            {isSearching ? "…" : "🔍"}
        </span>
        </button>

      </div>
    </div>
  );
}
