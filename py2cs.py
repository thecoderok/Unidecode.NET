import os, re

# this python script generates the unidecoder-decodemap.txt file 
# from the original python source. to make it run, you need 
# to download this directory from the original repository:
# https://github.com/avian2/unidecode/tree/master/unidecode
# and extract it in a folder named "py-codes"
# when run, it will generate a file named unidecoder-decodemap.txt 
# that must be copied in the assets folder. 
# this file will be included in the assembly and used in the static
# constructor of Unidecoder class.

d = "py-codes" # https://github.com/avian2/unidecode/tree/master/unidecode
print("working...")

fp = open("unidecoder-decodemap.txt", "w")

def formatch(ch, cc):
    ch = ch.replace("\r", "")
    ch = ch.replace("\\", "\\\\")
    ch = ch.replace("\"", "\\\"")
    ch = ch.replace("\n", '"\\n"')
    return ch if cc > 31 else "\\u" + ('%x' % cc).rjust(4, '0')

for file in [file for file in os.listdir(d) if not file in [".", ".."]]:
    m = re.search('x(.{3})\.py$', file)
    if m:
        data = __import__(d + "." + file[0:-3], [], [], ['data']).data
        missing = 256 - len(data)
        if missing != 0:
            fill = "[?]" if data[-1] == "[?]" else ""
            data += (fill,)*missing
        assert len(data) == 256
        c = 0
        idx = int(m.group(1), 16)
        num = idx * 256
        fp.write('%3s\t' % (idx))
        for ch in data:
            if ch is None:
                fp.write('""');
            else:
                fp.write('"%s"' % (formatch(ch, num + c)))
            if c<255:
                fp.write('\t')
            c = c + 1
        fp.write('\n')

print("converted!")

