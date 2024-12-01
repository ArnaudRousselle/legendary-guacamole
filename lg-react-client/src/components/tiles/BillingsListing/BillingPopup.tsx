import { Button, Typography } from "@mui/material";
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
      body = <Typography>Ajouter</Typography>;
      footer = (
        <Button
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
        </Button>
      );
      break;
    case "edit":
      title = "Modification d'une transaction";
      body = <Typography>Modifier</Typography>;
      footer = (
        <Button
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
        </Button>
      );
      break;
    case "delete":
      title = "Suppression d'une transaction";
      body = (
        <Typography>
          Etes-vous sûr de vouloir supprimer cette ligne ?
        </Typography>
      );
      footer = (
        <Button
          type="button"
          disabled={isDeleteBillingPending}
          onClick={() => deleteBilling({ id: args.billingId })}
        >
          Valider
        </Button>
      );
      break;
  }

  return (
    <Popup title={title} width={600} onClose={onClose} footer={footer}>
      {body}
    </Popup>
  );
};
