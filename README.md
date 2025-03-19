# spac_uge_4

# Dokumentation

## Database istedet for excel
Da det er meget næmmere at lave ændre i en database tabel end det er at ændre i en excel fil programatisk lavede jeg datasettet om til en database.  
Dette kevede dog noget rensing af dataen. Der var 2 ting der skull til for at rense dataen
1.  da der var links der havede ```;``` i sig og når man eksoportere til .csv på, ivertifal danske computere, kommer kolone sepratoerne ud som ```;```
2. ikke gyldige links, der var links der ikke havede en rigtig start af linket kastede det fejl, dette kunne sikker ungås hvis det blved lavet i noget andent end c#

## C# vs python
Jeg har valgt at lave det i C# da det er det programmerings sprog jeg kender bedst til.  
Valget af programmerinssprog gjorde det muglit at lave pdferne asyncorn så pogrammet kan fortsætte imens storre pdf filer blev lavet