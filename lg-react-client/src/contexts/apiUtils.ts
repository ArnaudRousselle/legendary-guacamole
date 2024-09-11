// fichier auto-généré !!
import {
  LegendaryGuacamoleWebApiApi,
} from "../api";
import { useApiRef } from "../hooks/useApiRef";

export interface IBaseApiContext {
  legendaryGuacamoleWebApiApi: LegendaryGuacamoleWebApiApi;
}

export const defaultBaseApiContextValues: IBaseApiContext = {
  legendaryGuacamoleWebApiApi: new LegendaryGuacamoleWebApiApi(),
};

export function useAllApisRef(getToken: () => string): IBaseApiContext {
  const legendaryGuacamoleWebApiApiRef = useApiRef(LegendaryGuacamoleWebApiApi, getToken);

  return {
    legendaryGuacamoleWebApiApi: legendaryGuacamoleWebApiApiRef.current,
  };
}
