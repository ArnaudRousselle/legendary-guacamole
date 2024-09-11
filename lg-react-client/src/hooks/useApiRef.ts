import { MutableRefObject, useRef } from "react";
import { BaseAPI, Configuration, FetchParams } from "../api";
import { getBasePath } from "../utils/basePath";

export function useApiRef<T extends BaseAPI>(
  apiClass: new (c: Configuration) => T,
  getToken: () => string
): MutableRefObject<T> {
  const apiRef = useRef(
    new apiClass(
      new Configuration({
        basePath: getBasePath(),
        fetchApi: async (url: string, init: RequestInit) => {
          init = init || {};
          const response = await window.fetch(url, init).catch((e) => {
            console.error(e);
            throw { message: "unexpected_error" };
          });
          if (response.status >= 400) {
            let jsonError: any;
            //todo ARNAUD: à tester
            try {
              jsonError = await response.json();
              console.error(jsonError);
            } catch (e) {
              throw { message: "Erreur inattendue" };
            }
            if ("message" in jsonError) throw { message: jsonError.message };
            else throw { message: "Erreur inconnue" };
          }
          return response;
        },
      })
    ).withMiddleware({
      pre: async ({ url, init }) => {
        const token = getToken();
        const headers: HeadersInit = token
          ? {
              ...init.headers,
              ["Authorization"]: "Bearer " + token,
            }
          : { ...init.headers };

        const params: FetchParams = {
          url,
          init: { ...init, headers },
        };
        return params;
      },
    })
  );
  return apiRef;
}
