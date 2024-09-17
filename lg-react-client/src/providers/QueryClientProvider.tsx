import {
  QueryClient,
  QueryClientProvider as TanStackQueryClientProvider,
} from "@tanstack/react-query";
import { PropsWithChildren } from "react";

const queryClient = new QueryClient();

export const QueryClientProvider = (props: PropsWithChildren) => {
  return (
    <TanStackQueryClientProvider client={queryClient}>
      {props.children}
    </TanStackQueryClientProvider>
  );
};
