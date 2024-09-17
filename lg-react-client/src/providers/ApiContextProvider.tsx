import { PropsWithChildren } from "react";
import { ApiContext } from "../contexts";
import { useAllApisRef } from "../contexts/apiUtils";

export const ApiContextProvider = (props: PropsWithChildren) => {
  const apisRef = useAllApisRef();
  return (
    <ApiContext.Provider
      value={{
        dummy: "",
        ...apisRef,
      }}
    >
      {props.children}
    </ApiContext.Provider>
  );
};
