import { CheckedCheckbox } from "./CheckedCheckbox";
import { useListBillingsQuery } from "../../../queries/billings";
import { DropDownButton } from "../../design/DropDownButton";
import { useState } from "react";
import { BillingPopupArgs } from "./types";
import { BillingPopup } from "./BillingPopup";
import { Classes, Icon, Spinner, Tooltip } from "@blueprintjs/core";
import { formatShortDate } from "../../../utils/formatShortDate";

export const BillingsListing = () => {
  const { isFetching, data: billings = [] } = useListBillingsQuery((res) =>
    [...res].sort((a, b) => {
      if (
        Math.abs(a.valuationDate.year - b.valuationDate.year) >= Number.EPSILON
      )
        return b.valuationDate.year - a.valuationDate.year;
      if (
        Math.abs(a.valuationDate.month - b.valuationDate.month) >=
        Number.EPSILON
      )
        return b.valuationDate.month - a.valuationDate.month;
      if (Math.abs(a.valuationDate.day - b.valuationDate.day) >= Number.EPSILON)
        return b.valuationDate.day - a.valuationDate.day;
      return a.id.localeCompare(b.id);
    })
  );

  const [popupArgs, setPopupArgs] = useState<BillingPopupArgs | null>(null);

  if (isFetching) return <Spinner />;

  //todo ARNAUD: voir pour faire un stickyHeader

  return (
    <>
      <div style={{ maxHeight: 440 }}>
        <table>
          <thead>
            <tr>
              <th align="center"></th>
              <th>Titre</th>
              <th align="center">Date de valeur</th>
              <th align="right">Montant</th>
              <th align="center">Commentaire</th>
              <th align="center">Pointé</th>
            </tr>
          </thead>
          <tbody>
            {billings.map((b) => (
              <tr key={b.id}>
                <td align="center">
                  <DropDownButton
                    icon="chevron-down"
                    actions={[
                      {
                        text: "Modifier",
                        onClick: () =>
                          setPopupArgs({ mode: "edit", billingId: b.id }),
                      },
                      {
                        text: "Supprimer",
                        onClick: () =>
                          setPopupArgs({ mode: "delete", billingId: b.id }),
                      },
                    ]}
                  />
                </td>
                <td>{b.title}</td>
                <td align="center">{formatShortDate(b.valuationDate)}</td>
                <td align="right">{b.amount.toFixed(2)}</td>
                <td align="center">
                  {b.comment ? (
                    <Tooltip
                      className={Classes.TOOLTIP_INDICATOR}
                      content={b.comment}
                    >
                      <Icon icon="info-sign" />
                    </Tooltip>
                  ) : null}
                </td>
                <td align="center">
                  <CheckedCheckbox billingId={b.id} checked={b.checked} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {popupArgs && (
        <BillingPopup onClose={() => setPopupArgs(null)} args={popupArgs} />
      )}
    </>
  );
};
