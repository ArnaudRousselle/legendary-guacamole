import { createContext, useContext } from "react";
import { defaultBaseApiContextValues, IBaseApiContext } from "./apiUtils";

export enum LogState {
  NotLogged = 0,
  Pending = 1,
  LoggedIn = 2,
}

interface IApiContext extends IBaseApiContext {
  logState: LogState;
  logIn: (token: string, storeTokenInLocalStorage: boolean) => void;
  logOut: () => void;
}

export const ApiContext = createContext<IApiContext>({
  logState: LogState.NotLogged,
  logIn: () => {},
  logOut: () => {},
  ...defaultBaseApiContextValues,
});

export function useApi() {
  return useContext(ApiContext);
}
