import {
  QueryClient,
  QueryClientProvider as TanStackQueryClientProvider,
} from "@tanstack/react-query";
import { PropsWithChildren } from "react";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false, //todo ARNAUD: mettre false seulement en dev
    },
  },
});

export const QueryClientProvider = (props: PropsWithChildren) => {
  return (
    <TanStackQueryClientProvider client={queryClient}>
      {props.children}
    </TanStackQueryClientProvider>
  );
};
