export type ListingFilters = {
  city?: string;
  guests?: string;
  minPrice?: string;
  maxPrice?: string;
  sort?: string;
};

export function filtersFromSearchParams(
  searchParams?: Record<string, string | string[] | undefined>
): ListingFilters {
  const get = (key: keyof ListingFilters) => {
    const v = searchParams?.[key];
    return typeof v === "string" ? v : "";
  };

  return {
    city: get("city"),
    guests: get("guests"),
    minPrice: get("minPrice"),
    maxPrice: get("maxPrice"),
    sort: get("sort"),
  };
}

export function toQueryString(filters: ListingFilters) {
  const qs = new URLSearchParams();

  if (filters.city?.trim()) qs.set("city", filters.city.trim());
  if (filters.guests?.trim()) qs.set("guests", filters.guests.trim());
  if (filters.minPrice?.trim()) qs.set("minPrice", filters.minPrice.trim());
  if (filters.maxPrice?.trim()) qs.set("maxPrice", filters.maxPrice.trim());
  if (filters.sort?.trim()) qs.set("sort", filters.sort.trim());

  const s = qs.toString();
  return s ? `?${s}` : "";
}