import { DtosShortDate } from "../api";

export function formatShortDate(
  date?: DtosShortDate | null | undefined
): string {
  if (!date) return "";
  return (
    date.day.toString().padStart(2, "0") +
    "/" +
    date.month.toString().padStart(2, "0") +
    "/" +
    date.year.toString().padStart(4, "0")
  );
}
