import { Icon } from "@blueprintjs/core";
import { BlueprintIcons_16Id } from "@blueprintjs/icons/lib/esm/generated/16px/blueprint-icons-16";
import { Fragment } from "react";

interface IProps {
  icon?: BlueprintIcons_16Id;
  actions: Array<{ text: string; onClick: () => void }>;
}

export const DropDownButton = ({ icon, actions }: IProps) => {
  //todo ARNAUD: à refaire
  return (
    <>
      {actions.map((a, i) => (
        <Fragment>
          {icon && <Icon icon={icon} />}
          <button key={i} type="button" onClick={a.onClick}>
            {a.text}
          </button>
        </Fragment>
      ))}
    </>
  );
  // const anchor = useRef<HTMLButtonElement>(null);
  // const [open, setOpen] = useState(false);
  // return (
  //   <>
  //     <IconButton ref={anchor} onClick={() => setOpen((p) => !p)}>
  //       {icon}
  //     </IconButton>
  //     {open && (
  //       <Menu
  //         id="basic-menu"
  //         anchorEl={anchor.current}
  //         open
  //         onClose={() => setOpen(false)}
  //         MenuListProps={{
  //           "aria-labelledby": "basic-button",
  //         }}
  //       >
  //         {actions.map((a, i) => (
  //           <MenuItem
  //             key={i}
  //             onClick={() => {
  //               a.onClick();
  //               setOpen(false);
  //             }}
  //           >
  //             {a.text}
  //           </MenuItem>
  //         ))}
  //       </Menu>
  //     )}
  //   </>
  // );
};
