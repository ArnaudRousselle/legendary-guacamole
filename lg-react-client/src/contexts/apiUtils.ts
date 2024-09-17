// fichier auto-généré !!
import {
  LegendaryGuacamoleWebApiApi,
} from "../api";
import { useApiRef } from "../hooks";

export interface IBaseApiContext {
  legendaryGuacamoleWebApiApi: LegendaryGuacamoleWebApiApi;
}

export const defaultBaseApiContextValues: IBaseApiContext = {
  legendaryGuacamoleWebApiApi: new LegendaryGuacamoleWebApiApi(),
};

export function useAllApisRef(): IBaseApiContext {
  const legendaryGuacamoleWebApiApiRef = useApiRef(LegendaryGuacamoleWebApiApi);

  return {
    legendaryGuacamoleWebApiApi: legendaryGuacamoleWebApiApiRef.current,
  };
}
