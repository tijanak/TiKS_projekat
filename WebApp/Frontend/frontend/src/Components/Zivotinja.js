import { useState, useEffect } from "react";
import BACKEND from "../config";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
export default function Zivotinja(state){
    const [zivotinja, setZivotinja] = useState(null); 
    useEffect(() => {
        
        fetch(`${BACKEND}Zivotinja/preuzmi/${state.id_posta}`, {
          method: "GET",
        })
          .then(response => response.json())
          .then(data => {
            
            // console.log(data);
            
            setZivotinja(data);
          })
          .catch(error => console.log(error));
      }, []);
    return (<>
    {zivotinja&&<Card variant="outlined">
        <Typography>
        Ime: {zivotinja.ime}
        </Typography>
        <Typography>
        Vrsta: {zivotinja.vrsta}
        </Typography>
        </Card>}
    </>);
}