export type BillingPopupArgs =
  | {
      mode: "edit";
      billingId: string;
    }
  | {
      mode: "delete";
      billingId: string;
    }
  | {
      mode: "create";
    };
