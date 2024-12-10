import { OverlaysProvider } from "@blueprintjs/core";
import BillingsListing from "./components/tiles/BillingsListing";
import { BillingsSummary } from "./components/tiles/BillingsSummary/BillingsSummary";
import { ApiContextProvider, QueryClientProvider } from "./providers";

function App() {
  return (
    <QueryClientProvider>
      <ApiContextProvider>
        <OverlaysProvider>
          <div>
            <BillingsListing />
          </div>
          <div>
            <BillingsSummary />
          </div>
        </OverlaysProvider>
      </ApiContextProvider>
    </QueryClientProvider>
  );
}

export default App;
