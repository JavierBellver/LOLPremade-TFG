function getImageByRank(rank) {

}

function dateOfBirthToAge(birthDate) {
    var bdate = new Date(birthDate.substr(0, 4), birthDate.substr(5, 2), birthDate.substr(7, 2))
    var ageDifMs = Date.now() - bdate.getTime();
    var ageDate = new Date(ageDifMs);
    return Math.abs(ageDate.getUTCFullYear() - 1970);
}

function compareRanks(rank, min, max) {
    if (min == '' && max == '' && rank == '') {
        return true;
    }

    var ranksInOrder = ["UNRANKED", "BRONZE V", "BRONZE IV", "BRONZE III", "BRONZE II", "BRONZE I", "SILVER V", "SILVER IV", "SILVER III", "SILVER II", "SILVER I", "GOLD V", "GOLD IV", "GOLD III", "GOLD II", "GOLD I", "PLATINUM V", "PLATINUM IV", "PLATINUM III", "PLATINUM II", "PLATINUM I", "DIAMOND V", "DIAMOND IV", "DIAMOND III", "DIAMOND II", "DIAMOND I", "MASTER", "CHALLENGER", "Finnish"];
    if (min == '') {
        min = "UNRANKED";
    }
    if (max == '') {
        max = "CHALLENGER";
    }
    var ranksInBetween = ranksInOrder.slice(ranksInOrder.indexOf(min), ranksInOrder.indexOf(max) + 1);
    if (min == max) {
        ranksInBetween.push(ranksInOrder[min])
        return rank == min
    }
    else {
        return (ranksInBetween.indexOf(rank) != -1)
    }
}

function compareAges(age, min, max) {
    if (min == '' && max == '') {
        return true;
    }
    else if (age >= 100) {
        return false;
    }
    else if (min != '' && max == '') {
        return age >= min
    }
    else if (min == '' && max != '') {
        return age <= max
    }
    else {
        return age >= min && age <= max
    }
}