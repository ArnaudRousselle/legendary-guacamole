const isDev = import.meta.env.DEV;

export function getBasePath() {
  if (isDev) return "http://localhost:5152";
  else return window.location.protocol + "//" + window.location.host;
}
