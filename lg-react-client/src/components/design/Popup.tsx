import { Dialog, DialogBody, DialogFooter } from "@blueprintjs/core";
import { BlueprintIcons_16Id } from "@blueprintjs/icons/lib/esm/generated/16px/blueprint-icons-16";
import { PropsWithChildren, ReactNode } from "react";

interface IProps extends PropsWithChildren {
  title: string;
  icon?: BlueprintIcons_16Id;
  onClose: () => void;
  open?: boolean;
  footer?: ReactNode | undefined;
}

export const Popup = ({
  title,
  icon,
  children,
  onClose,
  open = true,
  footer,
}: IProps) => {
  return (
    <Dialog title={title} icon={icon} isOpen={open} onClose={onClose}>
      <DialogBody>{children}</DialogBody>
      {footer && <DialogFooter actions={footer} />}
    </Dialog>
  );
};
