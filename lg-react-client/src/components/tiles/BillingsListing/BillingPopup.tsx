import { BillingPopupArgs } from "./types";
import {
  useAddBillingQuery,
  useDeleteBillingQuery,
  useEditBillingQuery,
} from "../../../queries/billings";
import { Popup } from "../../design/Popup";

interface IProps {
  args: BillingPopupArgs;
  onClose: () => void;
}

export const BillingPopup = ({ args, onClose }: IProps) => {
  const { mutate: addBilling, isPending: isAddBillingPending } =
    useAddBillingQuery(onClose);
  const { mutate: editBilling, isPending: isEditBillingPending } =
    useEditBillingQuery(onClose);
  const { mutate: deleteBilling, isPending: isDeleteBillingPending } =
    useDeleteBillingQuery(onClose);

  let title = "";
  let body = <></>;
  let footer = <></>;

  switch (args.mode) {
    case "create":
      title = "Ajout d'une transaction";
      body = <p>Ajouter</p>;
      footer = (
        <button
          type="button"
          disabled={isAddBillingPending}
          onClick={() =>
            addBilling({
              amount: 666,
              checked: false,
              isSaving: false,
              title: "test React",
              valuationDate: {
                day: 10,
                month: 6,
                year: 2025,
              },
            })
          }
        >
          Valider
        </button>
      );
      break;
    case "edit":
      title = "Modification d'une transaction";
      body = <p>Modifier</p>;
      footer = (
        <button
          type="button"
          disabled={isEditBillingPending}
          onClick={() =>
            editBilling({
              id: args.billingId,
              amount: 666,
              checked: false,
              isSaving: false,
              title: "test React",
              valuationDate: {
                day: 10,
                month: 6,
                year: 2025,
              },
            })
          }
        >
          Valider
        </button>
      );
      break;
    case "delete":
      title = "Suppression d'une transaction";
      body = <p>Etes-vous sûr de vouloir supprimer cette ligne ?</p>;
      footer = (
        <button
          type="button"
          disabled={isDeleteBillingPending}
          onClick={() => deleteBilling({ id: args.billingId })}
        >
          Valider
        </button>
      );
      break;
  }

  return (
    <Popup title={title} onClose={onClose} footer={footer}>
      {body}
    </Popup>
  );
};
