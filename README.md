Σύντομες οδηγίες για το testing του console app:

Screenshot από τον κώδικα του program.cs για το Owner entity for reference: https://prnt.sc/l1Xf5NNzux4p

- Για κάθε Entity ξεκινάμε με το μαζικό Create (πχ. γραμμή 28) που φτιάχνει 5 αντικείμενα και αφού γεμίσουμε τη βάση με ορισμένα δεδομένα δοκιμάζουμε και τις υπόλοιπες μεθόδους του Service.
- Κάθε Entity στο program.cs έχει στο 1ο μέρος του τα Create/Update/Delete method calls του, τα οποία κάνουμε uncomment και τα τρέχουμε ένα-ένα, βάζοντας πρώτα στις παραμέτρους τους τις τιμές της επιλογής μας.
Στο τέλος κάθε εντολής, υπάρχει σε υποσχόλιο εξήγηση για το τι παράμετρο θέλουμε σε κάθε εντολή. Π.χ. στη γραμμή 31, έχουμε το παρακάτω:
//PropertyCustomResponse response = ownerService.UpdatePropertyOwner(2, owner1); //parameters: (owner id, new owner)
Βλέπουμε στο τέλος ότι η πρώτη παράμετρος είναι το owner id και η δεύτερη το owner object που φτιάξαμε στην αρχή του αρχείου. Αυτά φαίνονται και στα Services αλλά τα συμπεριέλαβα και στο Program.cs για τη διευκόλυνσή σας.

- Το δεύτερο κομμάτι κώδικα στο program.cs για κάθε Entity περιλαμβάνει τα view/get/search methods όπως αυτό που βλέπουμε στην εικόνα που ακολουθεί στο σχετικό screenshot: https://prnt.sc/4LWiYGGdfLut
- Όταν έρθει η ώρα να εκτελέσουμε μια συγκεκριμένη μέθοδο, την κάνουμε uncomment και βάζουμε σαν παράμετρο την τιμή/τιμές που θέλουμε.
