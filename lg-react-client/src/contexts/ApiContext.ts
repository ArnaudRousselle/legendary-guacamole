import { createContext, useContext } from "react";
import { defaultBaseApiContextValues, IBaseApiContext } from "./apiUtils";

interface IApiContext extends IBaseApiContext {
  dummy: string;
}

export const ApiContext = createContext<IApiContext>({
  dummy: "",
  ...defaultBaseApiContextValues,
});

export function useApi() {
  return useContext(ApiContext);
}
